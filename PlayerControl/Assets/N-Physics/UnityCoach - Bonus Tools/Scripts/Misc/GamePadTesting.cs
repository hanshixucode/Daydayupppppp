//
//  GamePadTesting.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCoach.Helpers
{
	[AddComponentMenu ("UnityCoach/Helpers/GamePadTesting")]
	[HelpURL ("http://unitycoach.ca/")]
	public class GamePadTesting : MonoBehaviour
	{
		bool [] keyStates = new bool[16];
		List<string> buttonLabels = new List<string> () {"Fire1", "Fire2", "Fire3", "Jump"};
		Dictionary <string, bool> buttonStates = new Dictionary<string, bool>();

		private bool connected = false;
		string [] controllers;

		IEnumerator CheckForControllers()
		{
			while (true)
			{
				controllers = Input.GetJoystickNames();
				if (!connected && controllers.Length > 0)
				{
					connected = true;
	//				Debug.Log("Connected");
				}
				else if (connected && controllers.Length == 0)
				{
					connected = false;
	//				Debug.Log("Disconnected");
				}
				yield return new WaitForSeconds(1f);
			}
		}

		void Awake ()
		{
			StartCoroutine (CheckForControllers());
			for (int i = 0 ; i < buttonLabels.Count ; i++)
				buttonStates.Add (buttonLabels[i], false);
		}

		void Update ()
		{
			for (int i = 0 ; i < keyStates.Length ; i++)
				keyStates[i] = Input.GetKey (string.Format("joystick button {0}", i));

			for (int i = 0 ; i < buttonStates.Count ; i++)
				buttonStates[buttonLabels[i]] = Input.GetButton (buttonLabels[i]);
		}

		void OnGUI()
		{
			GUILayout.Label("GamePad Connected : " + connected);
			if (connected)
			{
				for (int i = 0 ; i < controllers.Length ; i++)
					GUILayout.Label(controllers[i]);
			}

			GUILayout.Space (32);

			for (int i = 0 ; i < keyStates.Length ; i++)
				GUILayout.Label (string.Format("JoystickButton{0} : {1}", i, keyStates[i]));
			
			for (int i = 0 ; i < buttonStates.Count ; i++)
				GUILayout.Label (string.Format("{0} : {1}", buttonLabels[i], buttonStates[buttonLabels[i]]));
		}
	}
}