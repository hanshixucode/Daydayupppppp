//
//  SceneLoader.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityCoach.Helpers
{
	[AddComponentMenu ("UnityCoach/Helpers/Scene Loader")]
	[HelpURL ("http://unitycoach.ca")]
	[DisallowMultipleComponent]
	public class SceneLoader : MonoBehaviour
	{
		public void LoadScene (int sceneIndex)
		{
			SceneManager.LoadScene (sceneIndex);
		}

		public void LoadScene (string sceneName)
		{
			SceneManager.LoadScene (sceneName);
		}

		public void LoadNext ()
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
		}

		public void Reload ()
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
	}
}