//
//  SpeedOMeter.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityCoach.Events;

// TODO : decouple the speed o meter from the Rigidbody

namespace NPhysics.Helpers
{
	[AddComponentMenu("N-Physics/Helpers/Speed O Meter")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	[RequireComponent (typeof (Rigidbody))]
	public class SpeedOMeter : MonoBehaviour
	{
		public enum Units {MpS, KmH, MpH};
		public enum Direction {Any, XZ, XY, YZ, Forward, Back, Up, Down, Right, Left};

		[Tooltip ("Speed Units.")]
		[SerializeField] Units _units = Units.KmH;

		[Tooltip ("Speed Direction.")]
		[SerializeField] Direction _direction;

		[Tooltip ("Is Speed measured relatively (in local space)")]
		[SerializeField] bool _relative;

		[SerializeField] private FloatEvent _onVelocityChanged;
		
		Rigidbody _rigidbody;
		Vector3 _velocity;
		float _magnitude;

		private float _speed;
		public float Speed
		{
			get { return _speed; }
			set
			{
				if (value != _speed)
				{
					_speed = value;
					_onVelocityChanged.Invoke(_speed);
				}
			}
		}

		void Awake ()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			_velocity = _relative ? transform.InverseTransformDirection(_rigidbody.velocity) : _rigidbody.velocity;

			switch (_direction)
			{
				case Direction.Any:
					_magnitude = _velocity.magnitude;
					break;
				case Direction.XZ:
					_magnitude = Vector3.ProjectOnPlane(_velocity, _relative ? transform.up : Vector3.up).magnitude;
					break;
				case Direction.XY:
					_magnitude = Vector3.ProjectOnPlane(_velocity, _relative ? transform.forward : Vector3.forward).magnitude;
					break;
				case Direction.YZ:
					_magnitude = Vector3.ProjectOnPlane(_velocity, _relative ? transform.right : Vector3.right).magnitude;
					break;
				case Direction.Forward:
					_magnitude = _velocity.z;
					break;
				case Direction.Back:
					_magnitude = -_velocity.z;
					break;
				case Direction.Up:
					_magnitude = _velocity.y;
					break;
				case Direction.Down:
					_magnitude = -_velocity.y;
					break;
				case Direction.Right:
					_magnitude = _velocity.x;
					break;
				case Direction.Left:
					_magnitude = -_velocity.x;
					break;
			}

			switch (_units)
			{
				case Units.MpS:
					Speed = _magnitude;
					break;
				case Units.KmH:
					Speed = _magnitude * 3.600f;
					break;
				case Units.MpH:
					Speed = _magnitude * 2.2369356f; // 0.621371f * 3.6f;
					break;
			}
		}
	}
}