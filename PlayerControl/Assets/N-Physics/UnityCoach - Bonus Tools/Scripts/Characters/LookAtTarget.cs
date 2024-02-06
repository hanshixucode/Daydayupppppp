//
//  LookAtTarget.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace UnityCoach.Characters
{
	[AddComponentMenu ("UnityCoach/Characters/Look At (Base)")]
	[HelpURL ("http://unitycoach.ca/")]
	[RequireComponent (typeof (Animator))]
	public class LookAtBase : MonoBehaviour
	{
		[SerializeField] [Range (0, 1)] float weight = 1f;
		[SerializeField] [Range (0, 1)] float bodyWeight = 0.3f;
		[SerializeField] [Range (0, 1)] float headWeight = 1f;
		[SerializeField] [Range (0, 1)] float eyesWeight = .2f;
		[SerializeField] [Range (0, 1)] float clampWeight = 0;

		private Animator _animator;
		protected Animator animator
		{
			get
			{
				if (!_animator)
					_animator = GetComponent<Animator>();
				return _animator;
			}
		}

		private Vector3 _lookat;
		public Vector3 lookat
		{
			get { return _lookat; }
			set
			{
				if (value != _lookat)
				{
					_lookat = value;
				}
			}
		}

		void OnAnimatorIK (int layerIndex)
		{
			animator.SetLookAtPosition(lookat);
			animator.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}
	}

	[AddComponentMenu ("UnityCoach/Characters/Look At Target")]
	[HelpURL ("http://unitycoach.ca/")]
	public class LookAtTarget : LookAtBase
	{
		[SerializeField] Transform target;

		void Update ()
		{
			lookat = target.position;
		}
	}
}