//
//  InitialAngularVelocity.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using System.Collections;
using UnityEngine;

namespace NPhysics
{
	/// <summary>
	/// Sets Rigidbody's Initial Angular Velocity upon Start.
	/// </summary>
	[AddComponentMenu("N-Physics/Rigidbody/Initial Angular Velocity")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[RequireComponent (typeof (Rigidbody))]
	public class InitialAngularVelocity : MonoBehaviour
	{
		[SerializeField] Vector3 _direction = Vector3.up;
		public Vector3 direction { get { return _direction; } set { _direction = value; } }

		[Tooltip ("Rotations per minute")]
		[SerializeField] float _rpm = 60f;
		public float rpm { get { return _rpm; } set { _rpm = value; } }

		public bool useWorldSpace;

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

//		IEnumerator Start ()
		void Start ()
		{
			rigidBody.angularVelocity = (useWorldSpace ? direction : transform.TransformDirection(direction)) * rpm * 6 * Mathf.Deg2Rad;

//			rigidBody.angularVelocity = (useWorldSpace ? direction : transform.TransformDirection(direction)) * rpm * 2 * Mathf.PI;

//			yield return new WaitForSeconds(1f);
//			Debug.Log(rigidBody.rotation.eulerAngles);
//			Debug.Break();
		}
	}
}