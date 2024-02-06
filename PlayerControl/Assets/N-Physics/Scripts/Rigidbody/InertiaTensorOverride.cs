//
//  InertiaTensorOverride.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace NPhysics
{
	[AddComponentMenu("N-Physics/Rigidbody/Inertia Tensor Override")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	[RequireComponent (typeof (Rigidbody))]
	public class InertiaTensorOverride : MonoBehaviour
	{
		// Inertia tensor must be larger than zero in all coordinates.
		// UnityEngine.Rigidbody:set_inertiaTensor(Vector3)
		static Vector3 minimumInertiaTensor = Vector3.one * 0.000001f;

		// Vector
		[SerializeField] private Vector3 _inertiaTensor;
		public Vector3 inertiaTensor
		{
			get { return _inertiaTensor; }
			set
			{
				_inertiaTensor = Vector3.Max(minimumInertiaTensor, value);
				rigidBody.inertiaTensor = _inertiaTensor;
			}
		}

		// Quaternion
		[HideInInspector] [SerializeField] private Quaternion _inertiaTensorRotation;
		public Quaternion inertiaTensorRotation
		{
			get { return _inertiaTensorRotation; }
			set
			{
				_inertiaTensorRotation = value;
//				_inertiaTensorEulerAngles = _inertiaTensorRotation.eulerAngles;
				rigidBody.inertiaTensorRotation = _inertiaTensorRotation;
			}
		}

		// Euler Angles, for human understanding only
		// Now in Custom Editor as GUI field, no longer needed
//		[SerializeField] Vector3 _inertiaTensorEulerAngles;

		Rigidbody _rigidBody;
		public Rigidbody rigidBody
		{
			get
			{
				if (!_rigidBody)
					_rigidBody = GetComponent<Rigidbody>();
				return _rigidBody;
			}
		}

		void Awake ()
		{
			rigidBody.inertiaTensorRotation = inertiaTensorRotation;
			rigidBody.inertiaTensor = inertiaTensor;
		}

		/* // Custom Editor going through Quaternion property, no longer needed
		void OnValidate ()
		{
			// updates rigid body's inertia tensor when value it input in the inspector
			// and reloads value when scene opens in the Editor
			inertiaTensorRotation = Quaternion.Euler(_inertiaTensorEulerAngles);
			inertiaTensor = _inertiaTensor;
		}
		//*/

		void Reset ()
		{
			// resets inertia tensor and reads value
			rigidBody.ResetInertiaTensor();
			inertiaTensorRotation = rigidBody.inertiaTensorRotation;
			inertiaTensor = rigidBody.inertiaTensor;
		}
	}
}