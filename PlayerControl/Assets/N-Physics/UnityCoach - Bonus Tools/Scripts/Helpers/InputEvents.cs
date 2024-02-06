//
//  InputEvents.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityCoach.Events;
using System;

namespace UnityCoach.Helpers
{
	[AddComponentMenu("UnityCoach/Helpers/Input Events")]
	public class InputEvents : MonoBehaviour
	{
		[Serializable]
		public class AxisEvent
		{
			public string inputName;
			public AnimationCurve mappingCurve = new AnimationCurve (new Keyframe[2] {new Keyframe (0, 0, 0, 0), new Keyframe (1, 1, 0, 0)});
			public FloatEvent uEvent;
		}

		[Serializable]
		public class ButtonEvent
		{
			public string inputName;
			public BoolEvent uEvent;
		}

		[Serializable]
		public class ButtonUpEvent
		{
			public string inputName;
			public UnityEvent uEvent;
		}

		[SerializeField] AxisEvent[] _axesEvents;
		[SerializeField] ButtonEvent[] _buttonEvents;
		[SerializeField] ButtonUpEvent[] _buttonUpEvents;

		void Update ()
		{
			if (_axesEvents.Length > 0)
			{
				for (int i = 0 ; i < _axesEvents.Length ; i++)
				{
					_axesEvents[i].uEvent.Invoke(_axesEvents[i].mappingCurve.Evaluate(Input.GetAxis(_axesEvents[i].inputName)));
				}
			}

			if (_buttonEvents.Length > 0)
			{
				for (int i = 0 ; i < _buttonEvents.Length ; i++)
				{
					_buttonEvents[i].uEvent.Invoke(Input.GetButton(_buttonEvents[i].inputName));
				}
			}

			if (_buttonUpEvents.Length > 0)
			{
				for (int i = 0 ; i < _buttonUpEvents.Length ; i++)
				{
					if (Input.GetButtonUp(_buttonUpEvents[i].inputName))
						_buttonUpEvents[i].uEvent.Invoke();
				}
			}
		}
	}
}