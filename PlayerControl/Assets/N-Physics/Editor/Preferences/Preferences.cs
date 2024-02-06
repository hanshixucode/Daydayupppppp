//
//  Preferences.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEditor;

namespace NPhysics.Editor
{
	public static class Preferences
	{
		static Color _handleColor;
		public static Color handleColor
		{
			get
			{
				if (_handleColor == Color.clear)
					_handleColor = GetJsonObjectFromEditorPrefs<Color>("HandleColor", Color.yellow);
				return _handleColor;
			}
			private set
			{
				if (value != _handleColor)
				{
					_handleColor = value;
					SetJsonObjectToEditorPrefs<Color>("HandleColor", _handleColor);
				}
			}
		}

		static float _handleSize = -1;
		public static float handleSize
		{
			get
			{
				if (_handleSize == -1)
					_handleSize = EditorPrefs.GetFloat("HandleSize", 0.1f);
				return _handleSize;
			}
			set
			{
				if (value != _handleSize)
				{
					_handleSize = value;
					EditorPrefs.SetFloat("HandleSize", _handleSize);
				}
			}
		}

		static float _handleSnap = -1;
		public static float handleSnap
		{
			get
			{
				if (_handleSnap == -1)
					_handleSnap = EditorPrefs.GetFloat("HandleSnap", 0.1f);
				return _handleSnap;
			}
			set
			{
				if (value != _handleSnap)
				{
					_handleSnap = value;
					EditorPrefs.SetFloat("HandleSnap", _handleSnap);
				}
			}
		}

		static Color _gravityColor;
		public static Color gravityColor
		{
			get
			{
				if (_gravityColor == Color.clear)
					_gravityColor = GetJsonObjectFromEditorPrefs<Color>("GravityColor", Color.yellow);
				return _gravityColor;
			}
			private set
			{
				if (value != _gravityColor)
				{
					_gravityColor = value;
					SetJsonObjectToEditorPrefs<Color>("GravityColor", _gravityColor);
				}
			}
		}

		[PreferenceItem("N Physics")]
		public static void PreferencesGUI()
		{
			EditorGUILayout.HelpBox (Informations.nPhysicsInfo, MessageType.None, true);
			handleColor = EditorGUILayout.ColorField("Handle Color", handleColor);
			handleSize = EditorGUILayout.FloatField("Handle Size", handleSize);
			gravityColor = EditorGUILayout.ColorField("Gravity Color", gravityColor);

			if (GUILayout.Button("Reset"))
				ResetToDefaults();
		}

		private static void ResetToDefaults ()
		{
			EditorPrefs.DeleteKey("HandleColor");
			_handleColor = Color.clear;
			EditorPrefs.DeleteKey("HandleSize");
			_handleSize = -1;
			EditorPrefs.DeleteKey("GravityColor");
			_gravityColor = Color.clear;
		}

		private static T GetJsonObjectFromEditorPrefs<T> (string key, object defaultValue)
		{
			return JsonUtility.FromJson<T>(EditorPrefs.GetString(key, JsonUtility.ToJson(defaultValue)));
		}

		private static void SetJsonObjectToEditorPrefs<T> (string key, T obj)
		{
			EditorPrefs.SetString(key, JsonUtility.ToJson(obj));
		}
	}

	public static class Informations
	{
		public const string HELP_URL = "http://unitycoach.ca/n-physics/";

		public static string nPhysicsInfo = "N Physics Preferences.";

		public static string centerOfMassPositionInfo = "N Physics - Center of Mass Position Editor.\n" +
			"This component allows modifying and saving rigidbody's center of mass.";

		public static string inertiaTensorOverrideInfo = "N Physics - Inertia Tensor Override.\n" +
			"This component allows editing Rigidbodies' inertia tensor rotation and strength.";

		public static string gravityVectorInfo = "N Physics - Gravity Vector.\n" +
			"This component allows changing Gravity direction and scale in the scene.";

		public static string initialVelocityInfo = "N Physics - Initial Velocity.\n" +
			"This component allows giving a Rigidbody an initial velocity.";

		public static string initialAngVelocityInfo = "N Physics - Initial Angular Velocity.\n" +
			"This component allows giving a Rigidbody an initial angular velocity.";

		public static string mediumDragTriggerInfo = "N Physics - Medium Drag.\n" +
			"This component changes rigidbodies' drag within its boundaries.";

		public static string mediumForceTriggerInfo = "N Physics - Medium Force.\n" +
			"This component adds force to rigidbodies within its boundaries.";
	}
}