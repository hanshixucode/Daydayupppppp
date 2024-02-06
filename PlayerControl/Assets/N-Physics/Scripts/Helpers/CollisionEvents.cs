//
//  CollisionEvents.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEngine.Events;
using UnityCoach.Events;

namespace NPhysics.Helpers
{
	[AddComponentMenu("N-Physics/Helpers/Collision Events")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
//	[RequireComponent (typeof (Collider))]
	public class CollisionEvents : MonoBehaviour
	{
		[SerializeField] float minimumMagnitude = 2f;
		[SerializeField] string _filterTag = "Player";
		[SerializeField] bool _collidesOnlyOnce;
		[SerializeField] bool _resetOnCollisionExit;

		[SerializeField] UnityEvent onCollisionEnter;
		[SerializeField] UnityEvent onCollisionExit;

		[SerializeField] string _animatorBoolName = "State";
		[SerializeField] AnimatorBoolEvent _animatorBoolEvent;
		int _animatorBoolNameId;

		[SerializeField] string _animatorCollisionEnterName = "Enter";
		[SerializeField] AnimatorTriggerEvent _animatorCollisionEnter;
		int _animatorCollisionEnterId;

		[SerializeField] string _animatorCollisionExitName = "Exit";
		[SerializeField] AnimatorTriggerEvent _animatorCollisionExit;
		int _animatorCollisionExitId;

		bool _collided;

		void Awake ()
		{
			_animatorBoolNameId = Animator.StringToHash(_animatorBoolName);
		}

		void OnCollisionEnter (Collision collision)
		{
			if (_collidesOnlyOnce && _collided)
				return;

			if (minimumMagnitude > collision.relativeVelocity.magnitude)
				return;
			
			if (_filterTag != string.Empty && !collision.gameObject.CompareTag(_filterTag))
				return;

			_collided = true;

			onCollisionEnter.Invoke();
			_animatorBoolEvent.Invoke(_animatorBoolNameId, true);
			_animatorCollisionEnter.Invoke(_animatorCollisionEnterId);
		}

		void OnCollisionExit (Collision collision)
		{
			if (!_collided)
				return;
			
			if (_filterTag != string.Empty && !collision.gameObject.CompareTag(_filterTag))
				return;

			onCollisionExit.Invoke();
			_animatorBoolEvent.Invoke(_animatorBoolNameId, false);
			_animatorCollisionExit.Invoke(_animatorCollisionExitId);

			if (_resetOnCollisionExit)
				_collided = false;
		}
	}
}