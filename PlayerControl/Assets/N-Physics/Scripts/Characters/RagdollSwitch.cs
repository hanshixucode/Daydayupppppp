//
//  RagdollSwitch.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

#undef DEBUG
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
#if UNITY_EDITOR
using System.Reflection;
#endif
using UnityCoach.Events;

namespace NPhysics.Characters
{
	[AddComponentMenu("N-Physics/Characters/Ragdoll Switch")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[RequireComponent (typeof (Animator))]
	public class RagdollSwitch : MonoBehaviour
	{
		#if UNITY_EDITOR
		[Tooltip ("This only works in the Editor. Toggle the isRagdoll property through scripting at Runtime.")]
		[SerializeField] KeyCode _testKey = KeyCode.R;
		#endif

		[SerializeField] bool _isRagdoll;

		[SerializeField] bool _disableRootChildrenCollisions = true;

		[SerializeField] float _limbVelocityMultiplier = 1;
		[SerializeField] float _limbAngularVelocityMultiplier = 1;

		[SerializeField] AnimatorUpdateMode _animatorUpdateMode;

		[SerializeField] BoolEvent _ragdollState;
		[SerializeField] BoolEvent _animatorState;
		[SerializeField] bool _recenter;

		bool _switchNextFixedUpdate;

		/// <summary>
		/// Switches between ragdoll and animator controlled.
		/// </summary>
		public bool isRagdoll
		{
			get { return _isRagdoll; }
			set
			{
				if (value != _isRagdoll)
				{
					_isRagdoll = value;
					_switchNextFixedUpdate = true;
				}
			}
		}

		private Animator _animator;
		private Rigidbody[] _rigidbodies;
		private Vector3[] _positions;
		private Vector3[] _rotations;

		void Awake ()
		{
			_animator = GetComponent<Animator>();
			Rigidbody rootbody = GetComponent<Rigidbody>();
			_rigidbodies = (from item in GetComponentsInChildren<Rigidbody>()
				where item != rootbody
				select item).ToArray();

			_positions = new Vector3[_rigidbodies.Length];
			_rotations = new Vector3[_rigidbodies.Length];

			if (_disableRootChildrenCollisions)
			{
				Collider[] rootColliders = GetComponents<Collider>();
				Collider[] allColliders = GetComponentsInChildren<Collider>();
				foreach (Collider rc in rootColliders)
					foreach (Collider c in allColliders)
						Physics.IgnoreCollision(rc, c);
			}
		}

		Vector3 GetVelocity (Vector3 currentPosition, Vector3 previousPosition, float timeDelta)
		{
			return (currentPosition - previousPosition) / timeDelta;
		}

		Vector3 GetAngularVelocity (Vector3 currentRotation, Vector3 previousRotation, float timeDelta)
		{
			return Quaternion.FromToRotation(previousRotation, currentRotation).eulerAngles / timeDelta;
		}

		Vector3 AverageBodyPosition (Rigidbody[] limbs)
		{
			Vector3 avg = Vector3.zero;
			for (int i = 0 ; i < limbs.Length ; i++)
				avg += limbs[i].position;
			avg /= limbs.Length;
			return avg;
		}

		void Switch (bool toRagdoll)
		{
			for (int i = 0 ; i < _rigidbodies.Length ; i++)
			{
				#if DEBUG
				Debug.Log(string.Format("{0}\t=> rigidbody's velocity before Kinematic Switch:{1}", _rigidbodies[i], _rigidbodies[i].velocity));
				#endif
				_rigidbodies[i].isKinematic = !toRagdoll;
				#if DEBUG
				Debug.Log(string.Format("{0}\t=> rigidbody's velocity after Kinematic Switch:{1}", _rigidbodies[i], _rigidbodies[i].velocity));
				#endif
			}

			if (_animator)
				_animator.enabled = !toRagdoll;

			for (int i = 0 ; i < _rigidbodies.Length ; i++)
			{
				#if DEBUG
				Debug.Log(string.Format("{0}\t=> rigidbody's velocity :{1}\t, recalculated velocity : {2}", _rigidbodies[i], _rigidbodies[i].velocity, GetVelocity(_rigidbodies[i].position, _positions[i], Time.fixedDeltaTime)));
				#endif
				_rigidbodies[i].velocity = GetVelocity(_rigidbodies[i].position, _positions[i], Time.fixedDeltaTime) * _limbVelocityMultiplier;
				_rigidbodies[i].angularVelocity = GetAngularVelocity(_rigidbodies[i].rotation.eulerAngles, _rotations[i], Time.fixedDeltaTime) * _limbAngularVelocityMultiplier;
			}

			if (!toRagdoll && _recenter)
				transform.position = AverageBodyPosition(_rigidbodies);

			_ragdollState.Invoke(toRagdoll);
			_animatorState.Invoke(!toRagdoll);

			_switchNextFixedUpdate = false;
		}

		/// <summary>
		/// Toggles Ragdoll state.
		/// </summary>
		public void Toggle ()
		{
			isRagdoll = !isRagdoll;
		}

		void FixedUpdate ()
		{
			if (_switchNextFixedUpdate)
				Switch(isRagdoll);
			
			for (int i = 0 ; i < _rigidbodies.Length ; i++)
			{
				_positions[i] = _rigidbodies[i].position;
				_rotations[i] = _rigidbodies[i].rotation.eulerAngles;
			}
		}

		#if UNITY_EDITOR
		void Update ()
		{
			if (Input.GetKeyUp(_testKey))
				Toggle();
		}

		private Animator animator
		{
			get
			{
				if (!_animator)
					_animator = GetComponent<Animator>();
				return _animator;
			}
		}

		void Reset ()
		{
			OnValidate();
		}

		void OnValidate ()
		{
			if (animator)
			{
				typeof(Animator).GetProperty("updateMode").SetValue(animator, _animatorUpdateMode, null);
				typeof(Animator).GetProperty("enabled").SetValue(animator, !_isRagdoll, null);
			}

			Rigidbody root = GetComponent<Rigidbody>();
			foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
			{
				if (r != root)
					r.isKinematic = !_isRagdoll;
			}
		}
		#endif
	}
}