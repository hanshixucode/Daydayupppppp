//
//  InitialVelocityEditor.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEditor;

namespace NPhysics.Editor
{
	[CustomEditor (typeof (InitialVelocity))]
	public class InitialVelocityEditor : UnityEditor.Editor
	{
		InitialVelocity _initialVelocity;

		Vector3 _direction;
		float _magnitude;
		bool _worldSpace;

		bool _edit;

		void OnEnable ()
		{
			_initialVelocity = target as InitialVelocity;
		}

		public override void OnInspectorGUI ()
		{
			EditorGUILayout.HelpBox (Informations.initialVelocityInfo, MessageType.None, true);

//			base.DrawDefaultInspector();

			//*
			EditorGUI.BeginChangeCheck();
			_direction = EditorGUILayout.Vector3Field("Direction", _initialVelocity.direction);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Initial Velocity");
				_initialVelocity.direction = _direction.normalized;
			}

			EditorGUI.BeginChangeCheck();
			_magnitude = EditorGUILayout.FloatField("Magnitude", _initialVelocity.magnitude);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Initial Velocity Magnitude");
				_initialVelocity.magnitude = _magnitude;
			}

			EditorGUI.BeginChangeCheck();
			_worldSpace = GUILayout.Toggle(_initialVelocity.useWorldSpace, "Use World Space", EditorStyles.toggle);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Initial Velocity Space");
				_initialVelocity.useWorldSpace = _worldSpace;
			}
			//*/

			_edit = GUILayout.Toggle(_edit, "Edit", EditorStyles.miniButton);
		}

		Vector3 handlePosition;
		Vector3 handleDirection;
		float size;

		void OnSceneGUI ()
		{
			if (!_initialVelocity.enabled)
				return;

			handleDirection = _initialVelocity.useWorldSpace ? _initialVelocity.direction : _initialVelocity.transform.TransformDirection(_initialVelocity.direction);
			handlePosition = _initialVelocity.rigidBody.worldCenterOfMass + handleDirection * _initialVelocity.magnitude;

			size = HandleUtility.GetHandleSize(_initialVelocity.rigidBody.worldCenterOfMass) * Preferences.handleSize * 2.5f;

			Handles.color = Preferences.handleColor;

			if (_edit)
			{
				EditorGUI.BeginChangeCheck();
				_direction = Handles.PositionHandle(handlePosition, _initialVelocity.transform.rotation);
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(target, "Change Initial Velocity");
					if (_initialVelocity.useWorldSpace)
						_initialVelocity.direction = (_direction - _initialVelocity.rigidBody.worldCenterOfMass).normalized;
					else
						_initialVelocity.direction = _initialVelocity.transform.InverseTransformDirection(_direction - _initialVelocity.rigidBody.worldCenterOfMass).normalized;
					
					_initialVelocity.magnitude = (_direction - _initialVelocity.rigidBody.worldCenterOfMass).magnitude;
				}
			}

			Handles.Slider(handlePosition, handleDirection, size, Handles.ConeHandleCap, Preferences.handleSnap);
			Handles.DrawDottedLine(_initialVelocity.rigidBody.worldCenterOfMass, handlePosition, 10f);
		}
	}
}