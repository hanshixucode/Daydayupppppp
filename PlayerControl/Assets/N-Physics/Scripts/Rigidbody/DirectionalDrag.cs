//
//  DirectionalDrag.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

// TODO : compare with simple velocity multiplier

namespace NPhysics
{
	/// <summary>
	/// Mimics Rigidbody Drag behaviour,
	/// with a few extra features.
	/// </summary>
	[AddComponentMenu ("N-Physics/Rigidbody/Directional Drag")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	[DefaultExecutionOrder (1000)]
	// Execution Order is of no help for calculting upcoming velocity
	// Forces are only applied after Fixed Update
	// velocity will only reflect Forces applied this frame on next FixedUpdate
	// but it's useful if other scripts changes the useGravity property in current frame
	[RequireComponent (typeof (Rigidbody))]
	public class DirectionalDrag : MonoBehaviour
	{
		[Tooltip ("Local Space Directional Drag coefficient.")]
		[SerializeField] Vector3 _drag;
		[SerializeField] bool _displayDragForce;

		/// <summary>
		/// Local Space Directional Drag coefficient.
		/// </summary>
		public Vector3 drag
		{
			get { return _drag; }
			set { _drag = value; }
		}

		private Rigidbody _rigidBody;
		private Rigidbody rigidBody
		{
			get
			{
				if (!_rigidBody)
					_rigidBody = GetComponent<Rigidbody>();
				return _rigidBody;
			}
		}

		private ConstantForce _constantForce;
		private bool _useConstantForce;

		Vector3 _expectedVelocity;
		Vector3 _expectedVelocityLocalSpace;
		Vector3 _dragForce;

		void Awake ()
		{
			_constantForce = GetComponent<ConstantForce>();
			_useConstantForce = _constantForce != null;
		}

		void FixedUpdate ()
		{
            if (!Physics.autoSimulation)
                return;

			// Figuring sum of forces for current fixed update and resulting velocity increase
			// Acceleration (m/s/s) = Net Force (N) / Mass (Kg)
			// Acceleration due to Gravity is then
			// Physics.gravity * mass / mass
			// Acceleration = Physics.gravity
			// Velocity(m/s) at Time t + Time.fixedDeltaTime =
			// rigidBody.velocity + Physics.gravity * Time.fixedDeltaTime

			_expectedVelocity = rigidBody.velocity;

			if (rigidBody.useGravity)
				_expectedVelocity += Physics.gravity * Time.fixedDeltaTime;

			if (_useConstantForce)
				_expectedVelocity += ((_constantForce.force + transform.TransformVector(_constantForce.relativeForce)) / rigidBody.mass) * Time.fixedDeltaTime;

			// at this point, there is no way to know if another script or component has added other forces to the rigidbody
			// as forces will only be applied after FixedUpdate
			// velocity at time t + Time.fixedDeltaTime cannot be calculated
			// this may result is a slight difference between this and built-in drag coefficient
			// Obviously, built-in AddForce methods use the built-in drag coefficient in a way similar to :
			// AddForce (requiredForce - requiredForce * drag * Time.fixedDeltaTime, forceMode);

			// display _expectedVelocity
//			Debug.DrawRay(rigidBody.worldCenterOfMass, _expectedVelocity, Color.yellow);

			// display drag values
//			Debug.DrawRay(rigidBody.worldCenterOfMass, transform.right * drag.x, Color.red);
//			Debug.DrawRay(rigidBody.worldCenterOfMass, transform.up * drag.y, Color.green);
//			Debug.DrawRay(rigidBody.worldCenterOfMass, transform.forward * drag.z, Color.blue);

			// countering expected velocity
			_expectedVelocityLocalSpace = transform.InverseTransformDirection(_expectedVelocity);
			_dragForce = transform.TransformDirection(Vector3.Scale(-_expectedVelocityLocalSpace, drag));

			// _display drag force
			if (_displayDragForce)
				Debug.DrawRay(rigidBody.worldCenterOfMass, _dragForce, Color.cyan);

			rigidBody.AddForce(_dragForce, ForceMode.Acceleration);
		}

		/// <summary>
		/// Custom version of AddForce.
		/// Counters added force with Drag.
		/// </summary>
		public void AddForce (Vector3 force, ForceMode mode = ForceMode.Force)
		{
			force = force - Vector3.Scale (force, transform.TransformVector(drag));
			rigidBody.AddForce (force * Time.fixedDeltaTime, mode);
		}

		/// <summary>
		/// Custom version of AddRelativeForce.
		/// Counters added force with Drag.
		/// </summary>
		public void AddRelativeForce (Vector3 force, ForceMode mode = ForceMode.Force)
		{
			force = force - Vector3.Scale (force, drag);
			rigidBody.AddRelativeForce (force * Time.fixedDeltaTime, mode);
		}
	}
}