//
//  InertiaTensorOverrideEditor.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEditor;

namespace NPhysics.Editor
{
	[CustomEditor (typeof (InertiaTensorOverride))]
	public class InertiaTensorOverrideEditor : UnityEditor.Editor
	{
		static Color[] _colors = new Color[3] {new Color(1, 0, 0, 0.5f), new Color(0, 1, 0, 0.5f), new Color(0, 0, 1, 0.5f)};

		InertiaTensorOverride _inertiaTensorOverride;
		Vector3 _right, _up, _forward;

		Vector3 _iTensor;
		Quaternion _rotation;
		Vector3 _eulerAngles;

		Vector3 _position;
		float _size;

		Vector3 _inertiaTensorToDegreesPerSecond;
		float _torqueReference;

//		bool _displayInfo;

		private bool _editRotation;

		void OnEnable ()
		{
			_inertiaTensorOverride = target as InertiaTensorOverride;
			_rotation = _inertiaTensorOverride.inertiaTensorRotation;
			_torqueReference = _inertiaTensorOverride.rigidBody.mass;
		}

		public override void OnInspectorGUI ()
		{
//			_displayInfo = EditorGUILayout.Foldout (_displayInfo, "Information");
//			if (_displayInfo)
				EditorGUILayout.HelpBox (Informations.inertiaTensorOverrideInfo, MessageType.None, true);
			
//			base.DrawDefaultInspector();

			// replaced Default Inspector with strong typed fields
			EditorGUI.BeginChangeCheck();
			_iTensor = EditorGUILayout.Vector3Field("Inertia Tensor", _inertiaTensorOverride.inertiaTensor);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Rigidbody Inertia Tensor");
				_inertiaTensorOverride.inertiaTensor = _iTensor;
			}

			EditorGUI.BeginChangeCheck();
			_eulerAngles = EditorGUILayout.Vector3Field("Inertia Tensor Rotation", _inertiaTensorOverride.inertiaTensorRotation.eulerAngles);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Change Rigidbody Inertia Tensor Rotation");
				_inertiaTensorOverride.inertiaTensorRotation = Quaternion.Euler(_eulerAngles);
			}

			_editRotation = GUILayout.Toggle(_editRotation, "Edit Inertia Tensor Rotation", EditorStyles.miniButton);

			_torqueReference = EditorGUILayout.FloatField("Torque Reference", _torqueReference);
		}

		void OnSceneGUI ()
		{
			_position = _inertiaTensorOverride.transform.TransformPoint(_inertiaTensorOverride.rigidBody.centerOfMass);
			_size = HandleUtility.GetHandleSize(_inertiaTensorOverride.rigidBody.worldCenterOfMass);
			
			if (_editRotation)
			{
				EditorGUI.BeginChangeCheck();
				_rotation = Handles.RotationHandle(_rotation, _position);
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(target, "Change Inertia Tensor Rotation");
					_inertiaTensorOverride.inertiaTensorRotation = _rotation;
				}
			}

			_right = _inertiaTensorOverride.inertiaTensorRotation * _inertiaTensorOverride.transform.right;
			_forward = _inertiaTensorOverride.inertiaTensorRotation * _inertiaTensorOverride.transform.forward;
			_up = _inertiaTensorOverride.inertiaTensorRotation * _inertiaTensorOverride.transform.up;

			_inertiaTensorToDegreesPerSecond = InertiaTensorToDegreesPerSecond (_inertiaTensorOverride.inertiaTensor, _torqueReference);
			_inertiaTensorToDegreesPerSecond.x *= (_inertiaTensorOverride.rigidBody.constraints & RigidbodyConstraints.FreezeRotationX) > 0 ? 0 : 1;
			_inertiaTensorToDegreesPerSecond.y *= (_inertiaTensorOverride.rigidBody.constraints & RigidbodyConstraints.FreezeRotationY) > 0 ? 0 : 1;
			_inertiaTensorToDegreesPerSecond.z *= (_inertiaTensorOverride.rigidBody.constraints & RigidbodyConstraints.FreezeRotationZ) > 0 ? 0 : 1;

			Handles.color = _colors[0];
			Handles.DrawSolidArc(_position, _right, _forward, _inertiaTensorToDegreesPerSecond.x, _size);
			Handles.color = _colors[1];
			Handles.DrawSolidArc(_position, _up, _forward, _inertiaTensorToDegreesPerSecond.y, _size);
			Handles.color = _colors[2];
			Handles.DrawSolidArc(_position, _forward, _up, _inertiaTensorToDegreesPerSecond.z, _size);
		}

		static Vector3 InertiaTensorToDegreesPerSecond (Vector3 inertiaTensor, float torqueReference)
		{
			return Vector3.Min(new Vector3 ((1 / inertiaTensor.x) * Mathf.Rad2Deg, (1 / inertiaTensor.y) * Mathf.Rad2Deg, (1 / inertiaTensor.z) * Mathf.Rad2Deg) * torqueReference, Vector3.one * 360);
		}
	}
}