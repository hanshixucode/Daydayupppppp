//
//  CenterOfMassLocator.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace NPhysics.Helpers
{
	/// <summary>
	/// Helper tool to display a rigid body's center of mass at runtime.
	/// </summary>
	[AddComponentMenu("N-Physics/Helpers/Center Of Mass Locator")]
	public class CenterOfMassLocator : MonoBehaviour
	{
		[SerializeField] Rigidbody rigidBody;

		void Awake ()
		{
			if (!rigidBody)
				rigidBody = GetComponentInParent<Rigidbody>();

			if (!rigidBody)
				rigidBody = transform.root.GetComponentInChildren<Rigidbody>();
			
			if (!rigidBody)
				enabled = false;
		}

		void Update ()
		{
			transform.position = rigidBody.worldCenterOfMass;
			transform.rotation = rigidBody.rotation;
		}
	}
}