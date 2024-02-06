//
//  CenterOfMassPosition.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace NPhysics
{
	/// <summary>
	/// Center of mass position.
	/// Allows editing Rigidbody's Center of Mass location.
	/// </summary>
	[AddComponentMenu("N-Physics/Rigidbody/Center of Mass Position")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	[RequireComponent (typeof (Rigidbody))]
	public class CenterOfMassPosition : MonoBehaviour
	{
		[SerializeField] private Vector3 _centerOfMass;
		/// <summary>
		/// Center of mass location in Local Space.
		/// </summary>
		public Vector3 centerOfMass
		{
			get { return _centerOfMass; }
			set
			{
				if (value != _centerOfMass)
				{
					_centerOfMass = value;
					rigidBody.centerOfMass = _centerOfMass;
				}
			}
		}

		Rigidbody _rigidBody;
		Rigidbody rigidBody
		{
			get
			{
				if (!_rigidBody)
					_rigidBody = GetComponent<Rigidbody>();
				return _rigidBody;
			}
		}

		void Awake ()
		{
			rigidBody.centerOfMass = centerOfMass;
		}

		/* // Custom Editor going through property, no longer needed
		void OnValidate ()
		{
			// updates rigid body's center of mass when value is input in the inspector
			// and reloads value when scene opens in the Editor
			rigidBody.centerOfMass = _centerOfMass;
		}
		//*/

		public void Reset ()
		{
			// resets center or mass and reads value
			rigidBody.ResetCenterOfMass();
			_centerOfMass = rigidBody.centerOfMass;
		}
	}
}