//
//  PhysicsSimulation.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

//#define RESTORE_JOINTS // Under Work, Do not uncomment
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NPhysics
{
	/// <summary>
	/// Physics Simulation Controls.
	/// Allows pausing, stepping forward and resetting Physics Simulation.
	/// </summary>
	[AddComponentMenu("N-Physics/Simulation/Simulation Controls")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	public class PhysicsSimulation : MonoBehaviour
	{
		[SerializeField] bool holdSimulation;
		[SerializeField] bool findAllRigidbodies = true;
		[SerializeField] bool sendStartMessageOnRestart;

        #if UNITY_EDITOR
        [SerializeField] bool displaySimulationStateInGameView;
        #endif

        [SerializeField] Rigidbody [] rigidbodies;

		Dictionary<Rigidbody, Vector3> positions;
		Dictionary<Rigidbody, Quaternion> rotations;

		// Forces don't comply to Physics Simulation state
		// Keeping a reference to all Forces to :
		// put them on hold when pausing simulation and
		// apply their forces when stepping through simulation
		ConstantForce [] forces;

		#if RESTORE_JOINTS
		Dictionary<Rigidbody, List<Joint>> joints;
		Dictionary<Rigidbody, List<Joint>> jointsBackup;
		#endif

		void Awake ()
		{
            forces = (from cf in FindObjectsOfType<ConstantForce>()
                      where cf.enabled == true
                      select cf).ToArray();

			if (holdSimulation)
			{
				Physics.autoSimulation = false;

				foreach (ConstantForce f in forces)
					f.enabled = false;
			}

			if (findAllRigidbodies)
				rigidbodies = FindObjectsOfType<Rigidbody>();

			positions = new Dictionary<Rigidbody, Vector3>();
			rotations = new Dictionary<Rigidbody, Quaternion>();

			#if RESTORE_JOINTS
			joints = new Dictionary<Rigidbody, List<Joint>>();
			jointsBackup = new Dictionary<Rigidbody, List<Joint>>();
			#endif

			foreach (Rigidbody rb in rigidbodies)
			{
				positions.Add(rb, rb.position);
				rotations.Add(rb, rb.rotation);

				#if RESTORE_JOINTS
				joints.Add(rb, new List<Joint> (rb.GetComponents<Joint>()));
				Joint[] rbJointsBackup = new Joint[joints.Count];
				joints[rb].CopyTo(rbJointsBackup);
				jointsBackup.Add(rb, new List<Joint>(rbJointsBackup));
				#endif
			}
		}

		/// <summary>
		/// Restarts Physics Simulation.
		/// </summary>
		[ContextMenu ("Restart")]
		public void Restart ()
		{
			foreach (Rigidbody rb in rigidbodies)
			{
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;

				rb.position = positions[rb];
				rb.rotation = rotations[rb];

				// TODO : reset rigid bodies broken constraints
				#if RESTORE_JOINTS
				if (joints[rb].Count > 0) // if this rb had joints
					for (int j = 0 ; j < joints[rb].Count ; j++) // for all its joints
						if (joints[rb][j] == null) // if the joint broke
						{
							var restoredJoint = rb.gameObject.AddComponent(jointsBackup[rb][j].GetType()) as Joint; // add a new joint
							// AddComponent does not take a source, can only add the type
							// JsonUtility does not support engine types
//							JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(jointsBackup[rb][j]), restoredJoint); // restore the joint settings
							// Restoring as many parameters as we can
							/*
							restoredJoint.connectedBody = jointsBackup[rb][j].connectedBody;
							restoredJoint.autoConfigureConnectedAnchor = jointsBackup[rb][j].autoConfigureConnectedAnchor;
							restoredJoint.anchor = jointsBackup[rb][j].anchor;
							restoredJoint.connectedAnchor = jointsBackup[rb][j].connectedAnchor;
							restoredJoint.breakForce = jointsBackup[rb][j].breakForce;
							restoredJoint.breakTorque = jointsBackup[rb][j].breakTorque;
							restoredJoint.axis = jointsBackup[rb][j].axis;
							restoredJoint.connectedMassScale = jointsBackup[rb][j].connectedMassScale;
							restoredJoint.massScale = jointsBackup[rb][j].massScale;
							restoredJoint.enablePreprocessing = jointsBackup[rb][j].enablePreprocessing;
							restoredJoint.enableCollision = jointsBackup[rb][j].enableCollision;
							//*/
						}
				#endif

				if (sendStartMessageOnRestart)
					rb.gameObject.SendMessage("Start", SendMessageOptions.DontRequireReceiver);
			}

			// if Physics simulation is on hold, step forward to update
			if (!Physics.autoSimulation)
			{
				StepForward();
			}
		}

		void ResetVelocities ()
		{
			foreach (Rigidbody rb in rigidbodies)
			{
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
			}
		}

		void ResetPositions ()
		{
			foreach (Rigidbody rb in rigidbodies)
			{
				rb.position = positions[rb];
				rb.rotation = rotations[rb];
			}
		}

        /// <summary>
        /// Runs Physics Simulation.
        /// </summary>
        [ContextMenu ("Run Simulation")]
		public void Run ()
		{
			if (Physics.autoSimulation)
				return;

			Physics.autoSimulation = true;

			foreach (ConstantForce f in forces)
				f.enabled = true;
		}

		/// <summary>
		/// Pauses Physics Simulation.
		/// </summary>
		[ContextMenu ("Pause Simulation")]
		public void Pause ()
		{
			if (!Physics.autoSimulation)
				return;
			
			Physics.autoSimulation = false;

			foreach (ConstantForce f in forces)
				f.enabled = false;
		}

		/// <summary>
		/// Steps forward given Time delta.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public void StepForward (float deltaTime = 0.02f)
		{
			if (Physics.autoSimulation)
				return;
			
			foreach (ConstantForce f in forces)
			{
				Rigidbody rb = f.gameObject.GetComponent<Rigidbody>();
				rb.AddForce(f.force);
				rb.AddRelativeForce(f.relativeForce);
				rb.AddTorque(f.torque);
				rb.AddRelativeTorque(f.relativeTorque);
			}

			Physics.Simulate (deltaTime);
		}

		/// <summary>
		/// Steps forward by Time.fixedDeltaTime.
		/// </summary>
		[ContextMenu ("Step Forward")]
		public void StepForward ()
		{
			StepForward (Time.fixedDeltaTime);
		}

		/// <summary>
		/// Steps forward given Time delta, by Timestep.
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		/// <param name="step">TimeStep.</param>
		public void StepForwardAccurate (float deltaTime = 1f, float step = 0.01f)
		{
			float t = deltaTime;
			while (t > 0)
			{
				StepForward (step);
				t -= step;
			}
		}

		/// <summary>
		/// Updates Rigidbodies Starting positions and rotations.
		/// </summary>
		[ContextMenu("Update Rigidbodies Start Positions")]
		public void UpdateRigidbodiesStartPositionsAndRotations()
		{
			if (rigidbodies.Length == 0)
				return;

			for (int i = 0 ; i < rigidbodies.Length ; i++)
			{
				positions[rigidbodies[i]] = rigidbodies[i].position;
				rotations[rigidbodies[i]] = rigidbodies[i].rotation;
			}
		}

		#if UNITY_EDITOR
		void OnGUI ()
		{
			if (displaySimulationStateInGameView)
                GUILayout.Label(string.Format("Physics Simulation State : {0}", Physics.autoSimulation ? "Running" : "On Hold"));
		}
		#endif
	}
}