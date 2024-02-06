//
//  CustomEvents.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEngine.Events;
using System;

namespace UnityCoach.Events
{
	[Serializable]
	public class BoolEvent : UnityEvent<bool> {}

	[Serializable]
	public class FloatEvent : UnityEvent<float> {}

	[Serializable]
	public class IntEvent : UnityEvent<int> {}

	[Serializable]
	public class Vector3Event : UnityEvent<Vector3> {}

	[Serializable]
	public class AnimatorBoolEvent : UnityEvent<int, bool> {}

	[Serializable]
	public class AnimatorTriggerEvent : UnityEvent<int> {}

	[Serializable]
	public class RigidbodyEvent : UnityEvent<Rigidbody> {}
}