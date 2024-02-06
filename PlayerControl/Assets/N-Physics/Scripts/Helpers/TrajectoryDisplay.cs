//
//  TrajectoryDisplay.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace NPhysics.Helpers
{
	[AddComponentMenu("N-Physics/Helpers/Trajectory Display")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
//	[RequireComponent (typeof (Rigidbody))]
	public class TrajectoryDisplay : MonoBehaviour
	{
		[SerializeField] Color _trajectoryColor = Color.yellow;
		[SerializeField] float _trajectoryDuration = 5f;

		Rigidbody _rigidbody;
		Vector3 _previousPosition;

		void Awake ()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		void Start ()
		{
			if (_rigidbody)
				_previousPosition = _rigidbody.worldCenterOfMass;
			else
				_previousPosition = transform.position;
		}

		void LateUpdate ()
		{
			if (_rigidbody)
			{
				Debug.DrawLine(_previousPosition, _rigidbody.worldCenterOfMass, _trajectoryColor, _trajectoryDuration);
				_previousPosition = _rigidbody.worldCenterOfMass;
			}
			else
			{
				Debug.DrawLine(_previousPosition, transform.position, _trajectoryColor, _trajectoryDuration);
				_previousPosition = transform.position;
			}
		}
	}
}