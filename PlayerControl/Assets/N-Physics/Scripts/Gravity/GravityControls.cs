//
//  GravityControls.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityCoach.Events;

namespace NPhysics
{
	[AddComponentMenu("N-Physics/Gravity/Controls")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	public class GravityControls : MonoBehaviour
	{
		[SerializeField] bool resetOnAwake;
		[SerializeField] Vector3 initialGravity = new Vector3 (0, -9.8f, 0);

		[SerializeField] FloatEvent onGravityXChanged, onGravityYChanged, onGravityZChanged;
		[SerializeField] Vector3Event onGravityChanged;

		private Vector3 _gravity;

		public float x
		{
			get { return _gravity.x; }
			set
			{
				if (value != _gravity.x)
				{
					_gravity.x = value;
					Physics.gravity = _gravity;
					onGravityXChanged.Invoke(_gravity.x);
					onGravityChanged.Invoke(_gravity);
				}
			}
		}

		public float y
		{
			get { return _gravity.y; }
			set
			{
				if (value != _gravity.y)
				{
					_gravity.y = value;
					Physics.gravity = _gravity;
					onGravityYChanged.Invoke(_gravity.y);
					onGravityChanged.Invoke(_gravity);
				}
			}
		}

		public float z
		{
			get { return _gravity.z; }
			set
			{
				if (value != _gravity.z)
				{
					_gravity.z = value;
					Physics.gravity = _gravity;
					onGravityZChanged.Invoke(_gravity.z);
					onGravityChanged.Invoke(_gravity);
				}
			}
		}

		public Vector3 gravity
		{
			get { return _gravity; }
			set
			{
				_gravity = value;
				Physics.gravity = _gravity;
				onGravityXChanged.Invoke(_gravity.x);
				onGravityYChanged.Invoke(_gravity.y);
				onGravityZChanged.Invoke(_gravity.z);
				onGravityChanged.Invoke(_gravity);
			}
		}

		public void DefaultGravity ()
		{
			gravity = new Vector3 (0, -9.8f, 0);
		}

		public void CustomGravity (float gravity = 9.8f)
		{
			this.gravity = new Vector3 (0, -gravity, 0);
		}

		public void NoGravity ()
		{
			gravity = Vector3.zero;
		}

		void Awake ()
		{
			if (resetOnAwake)
				gravity = initialGravity;
			else
				gravity = Physics.gravity;
		}
	}
}