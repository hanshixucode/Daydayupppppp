//
//  SlowMotion.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEngine.Events;

namespace UnityCoach.Helpers
{
	[AddComponentMenu ("UnityCoach/Helpers/Slow Motion")]
	[HelpURL ("http://unitycoach.ca/")]
	public class SlowMotion : MonoBehaviour
	{
		[SerializeField] float slowMotionScale = .3f;
		[SerializeField] float slowMotionDuration = 3f;
		[SerializeField] AnimationCurve slowMotionCurve = new AnimationCurve (new Keyframe [4] {new Keyframe (0, 1, 0, 0), new Keyframe (0.2f, 0, 0, 0), new Keyframe (0.8f, 0, 0, 0), new Keyframe (1, 1, 0, 0)});

		[SerializeField] UnityEvent onSlowMotionEnd;

		float slowMoTimer;

		void Awake ()
		{
			enabled = false;
		}

		void OnEnable ()
		{
			slowMoTimer = 0;
			enabled = true;
		}

		void OnDisable ()
		{
			Time.timeScale = 1;
		}

		void Update ()
		{
			slowMoTimer += Time.deltaTime;
			Time.timeScale = Mathf.Lerp(slowMotionScale, 1, slowMotionCurve.Evaluate(Mathf.InverseLerp(0, slowMotionDuration, slowMoTimer)));

			if (Time.timeScale == 1)
			{
				onSlowMotionEnd.Invoke();
				enabled = false;
			}
		}
	}
}