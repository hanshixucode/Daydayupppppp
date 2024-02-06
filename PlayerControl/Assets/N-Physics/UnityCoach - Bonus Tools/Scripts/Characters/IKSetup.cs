//
//  IKSetup.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using System;

namespace UnityCoach.Characters
{
	//[ExecuteInEditMode]
	[AddComponentMenu("UnityCoach/Characters/IK Setup")]
	[HelpURL ("http://unitycoach.ca/")]
	[DefaultExecutionOrder (100)]
	[RequireComponent (typeof (Animator))]
	public class IKSetup : MonoBehaviour
	{
		[Serializable]
		public class IkGoalSetup
		{
			public AvatarIKGoal ikGoal;
			public Transform target;
			[Range (0, 1)] public float weight = 1;
		}

		[SerializeField] IkGoalSetup [] goalSetups = new IkGoalSetup [4];

		Animator _animator;
		Animator animator
		{
			get
			{
				if (!_animator)
					_animator = GetComponent<Animator>();
				return _animator;
			}
		}

		void OnAnimatorIK(int layerIndex)
		{
			foreach (IkGoalSetup g in goalSetups)
			{
				animator.SetIKPositionWeight(g.ikGoal, g.weight);
				animator.SetIKPosition(g.ikGoal, g.target.position);
				animator.SetIKRotationWeight(g.ikGoal, g.weight);
				animator.SetIKRotation(g.ikGoal, g.target.rotation);
			}
		}
	}
}