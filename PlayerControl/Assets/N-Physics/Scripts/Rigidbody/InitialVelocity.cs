//
//  InitialVelocity.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

// TODO : add option for delay

namespace NPhysics
{
	/// <summary>
	/// Sets Rigidbody's Initial Velocity upon Start.
	/// </summary>
	[AddComponentMenu("N-Physics/Rigidbody/Initial Velocity")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[RequireComponent (typeof (Rigidbody))]
	public class InitialVelocity : MonoBehaviour
	{
		[SerializeField] Vector3 _direction = Vector3.up;
		public Vector3 direction { get { return _direction; } set { _direction = value; } }

		[SerializeField] float _magnitude = 1;
		public float magnitude { get { return _magnitude; } set { _magnitude = value; } }

		[SerializeField] bool _useWorldSpace;
		public bool useWorldSpace { get { return _useWorldSpace; } set { _useWorldSpace = value; } }

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

		// Done through custom editor
//		void OnValidate ()
//		{
//			_direction.Normalize();
//		}

		void Start ()
		{
			rigidBody.velocity = (useWorldSpace ? direction : transform.TransformDirection(direction)) * magnitude;
		}
	}
}