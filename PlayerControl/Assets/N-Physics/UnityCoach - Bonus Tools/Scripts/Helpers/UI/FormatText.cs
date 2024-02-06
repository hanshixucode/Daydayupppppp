//
//  FormatText.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Text.RegularExpressions;

namespace UnityCoach.Helpers
{
	[AddComponentMenu ("UnityCoach/Helpers/UI/Format Text")]
	[HelpURL ("http://unitycoach.ca")]
	[DisallowMultipleComponent]
	[RequireComponent (typeof (Text))]
	public class FormatText : MonoBehaviour
	{
		[Multiline] [SerializeField] private string format = "input text : {0}";

		private Text _text;

		void Awake ()
		{
			_text = GetComponent<Text>();
		}

		public void Format (string input)
		{
			_text.text = Regex.Unescape (string.Format (format, input));
		}

		public void Format (int input)
		{
			_text.text = Regex.Unescape (string.Format (format, input.ToString("N0", CultureInfo.CurrentCulture)));
		}

		public void Format (float input)
		{
			_text.text = Regex.Unescape (string.Format (format, input.ToString("N", CultureInfo.CurrentCulture)));
		}
	}
}