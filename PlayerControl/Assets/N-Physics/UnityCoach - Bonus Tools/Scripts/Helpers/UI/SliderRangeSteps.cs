//
//  SliderRangeSteps.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// UNDONE : removed offset (non multiplier based mode), dodgy and not very useful

namespace UnityCoach.Helpers
{
	[AddComponentMenu("UnityCoach/Helpers/UI/Slider Range Steps")]
	[HelpURL("http://unitycoach.ca")]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Slider))]
	public class SliderRangeSteps : MonoBehaviour, IPointerUpHandler
	{
		[SerializeField] float logBase = 10f;
		[SerializeField] float minLimit = 0f;
		[SerializeField] float maxLimit = 1000f;

		Slider slider;
		float initMin, initMax, initValue;

		void Awake()
		{
			slider = GetComponent<Slider>();
			initMin = slider.minValue;
			initMax = slider.maxValue;
			initValue = slider.value;
		}

		#region IPointerUpHandler
		public void OnPointerUp(PointerEventData eventData)
		{
			// if value is approx. the initial value, reset range to init range
			if (Mathf.Approximately(slider.value, initValue))
			{
				slider.minValue = initMin;
				slider.maxValue = initMax;
				return;
			}

			float upper = Mathf.Clamp(Mathf.Pow(logBase, Mathf.Ceil(Mathf.Log(Mathf.Floor(Mathf.Abs(slider.value)) + 1f, logBase))), minLimit, maxLimit) * Mathf.Sign(slider.value);
			float lower = Mathf.Clamp(Mathf.Floor(Mathf.Pow(logBase, Mathf.Ceil(Mathf.Log(Mathf.Floor(Mathf.Abs(slider.value)) + 1f, logBase)) - 2)), minLimit, maxLimit) * Mathf.Sign(slider.value);

			bool positive = Mathf.Sign(slider.value) > 0;
			slider.maxValue = positive ? upper : lower;
			slider.minValue = positive ? lower : upper;
		}
		#endregion
	}
}