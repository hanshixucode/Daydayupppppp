//
//  CenterOfMassPositionEditor.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEditor;

namespace NPhysics.Editor
{
	[CustomEditor (typeof (CenterOfMassPosition))]
	public class CenterOfMassPositionEditor : UnityEditor.Editor
	{
		CenterOfMassPosition _centerOfMassPosition;

//		SerializedProperty _centerOfMass;

//		bool _displayInfo;
		bool _edit;
		Vector3 _worldPosition;
		Vector3 _localPosition;

		void OnEnable ()
		{
			_centerOfMassPosition = target as CenterOfMassPosition;
//			_centerOfMass = serializedObject.FindProperty("_centerOfMass");
		}

		public override void OnInspectorGUI ()
		{
//			_displayInfo = EditorGUILayout.Foldout (_displayInfo, "Information");
//			if (_displayInfo)
				EditorGUILayout.HelpBox (Informations.centerOfMassPositionInfo, MessageType.None, true);

//			base.DrawDefaultInspector();

			// replaced Default Inspector with Property field
//			EditorGUILayout.PropertyField(_centerOfMass);
//			serializedObject.ApplyModifiedProperties();

			// replaced Property field with strong typed field
			EditorGUI.BeginChangeCheck();
			_localPosition = EditorGUILayout.Vector3Field("Center of Mass Position", _centerOfMassPosition.centerOfMass);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Rigidbody Position");
				_centerOfMassPosition.centerOfMass = _localPosition;
			}

			GUILayout.BeginHorizontal();
			_edit = GUILayout.Toggle(_edit, "Edit", EditorStyles.miniButtonLeft);

			if (GUILayout.Button("Reset", EditorStyles.miniButtonRight))
				_centerOfMassPosition.Reset();
			GUILayout.EndHorizontal();
		}

		void OnSceneGUI ()
		{
			_worldPosition = _centerOfMassPosition.transform.TransformPoint(_centerOfMassPosition.centerOfMass);
			float size = HandleUtility.GetHandleSize(_worldPosition) * Preferences.handleSize;

			Handles.color = Preferences.handleColor; //Color.yellow;

			if (!_edit)
			{
				Handles.DrawWireDisc (_worldPosition, -SceneView.currentDrawingSceneView.camera.transform.forward, size);
				return;
			}

			EditorGUI.BeginChangeCheck();
			Vector3 position = Handles.PositionHandle(_worldPosition, _centerOfMassPosition.transform.rotation);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Rigidbody Position");
				_centerOfMassPosition.centerOfMass = (_centerOfMassPosition.transform.InverseTransformPoint(position));
			}

			Handles.DrawSolidDisc(_worldPosition, -SceneView.currentDrawingSceneView.camera.transform.forward, size);
		}
	}
}