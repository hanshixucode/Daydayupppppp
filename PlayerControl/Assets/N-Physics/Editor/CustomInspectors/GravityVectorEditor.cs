//
//  GravityVectorEditor.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEditor;

namespace NPhysics.Editor
{
	[CustomEditor (typeof (GravityVector))]
	public class GravityVectorEditor : UnityEditor.Editor
	{
		GravityVector _gravityVector;
		float _size;

		void OnEnable ()
		{
			_gravityVector = target as GravityVector;
		}

		void OnSceneGUI ()
		{
			_size = HandleUtility.GetHandleSize(_gravityVector.transform.position) * Preferences.handleSize;

			Handles.color = Preferences.gravityColor;

			// speed handle
			EditorGUI.BeginChangeCheck();
			Vector3 gravity = Handles.Slider(_gravityVector.transform.position - _gravityVector.transform.up * _gravityVector.gravityScale, -_gravityVector.transform.up, _size, Handles.ConeHandleCap, Preferences.handleSnap);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Velocity Value");
				_gravityVector.gravityScale = (gravity - _gravityVector.transform.position).magnitude;
			}
			Handles.DrawDottedLine(_gravityVector.transform.position, gravity, 10f);
		}
	}
}