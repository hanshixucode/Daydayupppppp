//
//  TriggerDragModifier.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : Add support for custom Drag Components

namespace NPhysics
{
	/// <summary>
	/// Modifies Drag coefficients of Rigidbodies entering the Trigger Zone.
	/// </summary>
	[AddComponentMenu("N-Physics/Medium/Drag")]
	[RequireComponent (typeof (Collider))]
	public class TriggerDragModifier : MonoBehaviour
	{
		public enum Mode {Set, Add, Multiply, None}

		[Tooltip ("The drag value change mode.\n" +
			"Set : set drag value to medium drag value.\n" +
			"Add : add medium drag value to original value.\n" +
			"Multiply : multiply original value by medium drag.\n" +
			"None : leaves drag as is.")]
		[SerializeField] Mode mode;

		[Tooltip ("The Drag value to assign Rigid Bodies entering this Medium.")]
		[SerializeField] float _mediumDrag = 1;

		[Tooltip ("The angular drag value change mode.\n" +
			"Set : set drag value to medium drag value.\n" +
			"Add : add medium drag value to original value.\n" +
			"Multiply : multiply original value by medium drag.\n" +
			"None : leaves drag as is.")]
		[SerializeField] Mode angularMode;

		[Tooltip ("The Drag value to assign Rigid Bodies entering this Medium.")]
		[SerializeField] float _mediumAngularDrag = 1;

		/// <summary>
		/// Drag value applied to ridid bodies within Medium.
		/// </summary>
		public float mediumDrag
		{
			get { return _mediumDrag; }
			set
			{
				if (value != _mediumDrag)
				{
					_mediumDrag = value;

					if (mediumBodies.Count == 0)
						return;

					// update all rigid bodies within medium when value changes
					for (int i = 0 ; i < mediumBodies.Count ; i++)
						UpdateRigidbodyDrag(mediumBodies[i], _mediumDrag);
				}
			}
		}

		public float mediumAngularDrag
		{
			get { return _mediumAngularDrag; }
			set
			{
				if (value != _mediumAngularDrag)
				{
					_mediumAngularDrag = value;

					if (mediumBodies.Count == 0)
						return;

					// update all rigid bodies within medium when value changes
					for (int i = 0 ; i < mediumBodies.Count ; i++)
						UpdateRigidbodyAngularDrag(mediumBodies[i], _mediumAngularDrag);
				}
			}
		}

		static Dictionary<Rigidbody, Vector2> dragValues = new Dictionary<Rigidbody, Vector2>();
		List <Rigidbody> mediumBodies = new List<Rigidbody>();

		void UpdateRigidbodyDrag (Rigidbody body, float drag)
		{
			switch (mode)
			{
				case Mode.Set:
					body.drag = drag;
					break;
				case Mode.Add:
					body.drag += drag;
					break;
				case Mode.Multiply:
					body.drag *= drag;
					break;
			}
		}

		void UpdateRigidbodyAngularDrag (Rigidbody body, float angularDrag)
		{
			switch (angularMode)
			{
				case Mode.Set:
					body.angularDrag = angularDrag;
					break;
				case Mode.Add:
					body.angularDrag += angularDrag;
					break;
				case Mode.Multiply:
					body.angularDrag *= angularDrag;
					break;
			}
		}

		void OnTriggerEnter (Collider other)
		{
			// checking for rigidbody
			if (!other.attachedRigidbody)
				return;

			// adding rigidbody to medium list, to update its value on property update
			mediumBodies.Add(other.attachedRigidbody);

			// storing original drag value
			if (!dragValues.ContainsKey(other.attachedRigidbody))
				dragValues.Add(other.attachedRigidbody, new Vector2 (other.attachedRigidbody.drag, other.attachedRigidbody.angularDrag));

			// changing drag value
			UpdateRigidbodyDrag (other.attachedRigidbody, mediumDrag);
			UpdateRigidbodyAngularDrag (other.attachedRigidbody, mediumAngularDrag);
		}

		void OnTriggerExit (Collider other)
		{
			// restoring drag and angular drag values
			if (dragValues.ContainsKey(other.attachedRigidbody))
			{
				other.attachedRigidbody.drag = dragValues[other.attachedRigidbody].x;
				other.attachedRigidbody.angularDrag = dragValues[other.attachedRigidbody].y;
			}

			// removing body from medium list
			mediumBodies.Remove(other.attachedRigidbody);

			// clear rigidbody drag values
			dragValues.Remove(other.attachedRigidbody);
		}
	}
}