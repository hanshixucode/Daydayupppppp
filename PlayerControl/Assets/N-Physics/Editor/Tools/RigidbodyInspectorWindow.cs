//
//  RigidbodyInspectorWindow.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NPhysics.Editor
{
	public class RigidbodyInspectorWindow : EditorWindow
	{
        bool lockSelection;
        bool verticalLayout;
        Vector2 svPos;

		List<Rigidbody> selectedRigidbodies = new List<Rigidbody>();

		[MenuItem ("N-Physics/Rigidbodies Inspector")]
		static void Init ()
		{
//			RigidbodyInspectorWindow window = EditorWindow.GetWindow<RigidbodyInspectorWindow>(true, "Rigidbodies Inspector");
//			window.Show();
			RigidbodyInspectorWindow window = EditorWindow.GetWindow<RigidbodyInspectorWindow>(false, "Rigidbodies Inspector");
			window.Show();
			window.OnSelectionChange();
		}

		void OnSelectionChange ()
		{
			if (!lockSelection)
			{
				selectedRigidbodies.Clear();
				foreach (GameObject g in Selection.gameObjects)
					selectedRigidbodies.AddRange(g.GetComponentsInChildren<Rigidbody>(true));
			}
		}

		void Awake ()
		{
			defaultGUIColor = GUI.color;
		}

		Color defaultGUIColor;
		Color disabledGUIColor = Color.gray;

		string valueFormat = "+00.000;-00.000";

		void OnGUI()
		{
            GUILayout.BeginHorizontal();
            lockSelection = EditorGUILayout.Toggle("LOCK SELECTION", lockSelection);
            verticalLayout = EditorGUILayout.Toggle("Vertical Layout", verticalLayout);
            GUILayout.EndHorizontal();

            GUILayout.Label("Selected Rigidbodies", EditorStyles.boldLabel);

            if (selectedRigidbodies.Count == 0)
                return;

            svPos = GUILayout.BeginScrollView(svPos);

            if (verticalLayout)
                GUILayout.BeginVertical();
            else
                GUILayout.BeginHorizontal();

			for (int i = 0 ; i < selectedRigidbodies.Count ; i++)
			{
				// cleaning list, in case of unhandled scene changes
				if (selectedRigidbodies[i] == null)
				{
					selectedRigidbodies.RemoveAt(i);
					return;
				}

				GUI.color = defaultGUIColor;
				GUILayout.BeginVertical();

				GUILayout.Label (selectedRigidbodies[i].name);

				if (!EditorApplication.isPlaying)
					GUI.color = disabledGUIColor;

				GUILayout.Label("Velocity (unit/s))", EditorStyles.boldLabel);
				GUILayout.BeginHorizontal();
				GUILayout.Label ("X : " + selectedRigidbodies[i].velocity.x.ToString(valueFormat));
				GUILayout.Label ("Y : " + selectedRigidbodies[i].velocity.y.ToString(valueFormat));
				GUILayout.Label ("Z : " + selectedRigidbodies[i].velocity.z.ToString(valueFormat));
				GUILayout.EndHorizontal();
				GUILayout.Label("Speed (unit/s)", EditorStyles.boldLabel);
				GUILayout.Label (selectedRigidbodies[i].velocity.magnitude.ToString(valueFormat));

				GUILayout.Label("Ang. Velocity (Radians/s)", EditorStyles.boldLabel);
				GUILayout.BeginHorizontal();
				GUILayout.Label ("X : " + selectedRigidbodies[i].angularVelocity.x.ToString(valueFormat));
				GUILayout.Label ("Y : " + selectedRigidbodies[i].angularVelocity.y.ToString(valueFormat));
				GUILayout.Label ("Z : " + selectedRigidbodies[i].angularVelocity.z.ToString(valueFormat));
				GUILayout.EndHorizontal();
				GUILayout.Label("Ang. Speed (Rpm)", EditorStyles.boldLabel);
				GUILayout.Label ((selectedRigidbodies[i].angularVelocity.magnitude * Mathf.Rad2Deg / 6f).ToString(valueFormat));

				GUILayout.EndVertical();
			}

            if (verticalLayout)
                GUILayout.EndVertical();
            else
                GUILayout.EndHorizontal();

            GUILayout.EndScrollView();
		}

		void Update ()
		{
//			if (EditorApplication.isPlaying)
				Repaint();
		}
	}
}