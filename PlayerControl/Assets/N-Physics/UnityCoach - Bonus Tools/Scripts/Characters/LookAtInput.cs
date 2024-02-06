//
//  LookAtInput.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace UnityCoach.Characters
{
	[AddComponentMenu ("UnityCoach/Characters/Look At Input")]
	[HelpURL ("http://unitycoach.ca/")]
	public class LookAtInput : LookAtBase
	{
		[SerializeField] string _horizonAxis = "Horizontal";
		[SerializeField] string _lookupAxis = "Vertical";
		[SerializeField] AnimationCurve _horizonAmplitude = new AnimationCurve(new Keyframe [2] { new Keyframe(-1, -90, 0, 0), new Keyframe(1, 90, 0, 0) });
		[SerializeField] AnimationCurve _verticalAmplitude = new AnimationCurve(new Keyframe [3] { new Keyframe(-1, -50, 0, 0), new Keyframe(0, 0, 0, 0), new Keyframe(1, 80, 0, 0) });
		[SerializeField] float _horizonHeight = 1.4f;

		private Vector3 _lookatDirection;
		float _lookatDistance = 1;
		Vector2 _input;

		[SerializeField] Transform _reference;

		void Reset ()
		{
			_reference = transform.root ? transform.root : animator.transform;
		}

		void Update ()
		{
			_input.x = Input.GetAxis(_horizonAxis);
			_input.y = Input.GetAxis(_lookupAxis);

			_lookatDirection.x = -Mathf.Sin(_horizonAmplitude.Evaluate(_input.x) * -Mathf.Deg2Rad);
			_lookatDirection.z = Mathf.Cos(_horizonAmplitude.Evaluate(_input.x) * -Mathf.Deg2Rad);
			_lookatDirection.y = -Mathf.Sin(_verticalAmplitude.Evaluate(_input.y) * -Mathf.Deg2Rad);

			lookat = _reference.TransformPoint(Vector3.up * _horizonHeight + _lookatDirection * _lookatDistance);

			Debug.DrawLine(_reference.TransformPoint(0, _horizonHeight, 0), lookat, Color.green);
		}
	}
}