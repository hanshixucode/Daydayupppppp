//
//  TriggerEvents.cs
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
	[AddComponentMenu("N-Physics/Helpers/Trigger Events")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
//	[RequireComponent (typeof (Collider))]
	public class TriggerEvents : MonoBehaviour
	{
		[SerializeField] string _filterTag = "Player";

		[SerializeField] UnityEvent onTriggerEnter;
		[SerializeField] UnityEvent onTriggerExit;

		[SerializeField] string _animatorBoolName = "State";
		[SerializeField] AnimatorBoolEvent _animatorBoolEvent;
		int _animatorBoolNameId;

		[SerializeField] string _animatorTriggerEnterName = "Enter";
		[SerializeField] AnimatorTriggerEvent _animatorTriggerEnter;
		int _animatorTriggerEnterId;

		[SerializeField] string _animatorTriggerExitName = "Exit";
		[SerializeField] AnimatorTriggerEvent _animatorTriggerExit;
		int _animatorTriggerExitId;

		void Awake ()
		{
			_animatorBoolNameId = Animator.StringToHash(_animatorBoolName);
			_animatorTriggerEnterId = Animator.StringToHash(_animatorTriggerEnterName);
			_animatorTriggerExitId = Animator.StringToHash(_animatorTriggerExitName);
		}

		void OnTriggerEnter (Collider other)
		{
			if (_filterTag != string.Empty && !other.CompareTag(_filterTag))
				return;

			onTriggerEnter.Invoke();
			_animatorBoolEvent.Invoke(_animatorBoolNameId, true);
			_animatorTriggerEnter.Invoke(_animatorTriggerEnterId);
		}

		void OnTriggerExit (Collider other)
		{
			if (_filterTag != string.Empty && !other.CompareTag(_filterTag))
				return;

			onTriggerExit.Invoke();
			_animatorBoolEvent.Invoke(_animatorBoolNameId, false);
			_animatorTriggerExit.Invoke(_animatorTriggerExitId);
		}
	}
}