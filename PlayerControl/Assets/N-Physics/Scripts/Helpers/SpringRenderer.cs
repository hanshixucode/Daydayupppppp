//
//  SpringRenderer.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace NPhysics.Helpers
{
	[AddComponentMenu ("N-Physics/Helpers/SpringJoint Renderer")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	[RequireComponent (typeof (LineRenderer))]
	public class SpringRenderer : MonoBehaviour
	{
		[SerializeField] SpringJoint _springJoint;
		public SpringJoint springJoint
		{
			get { return _springJoint; }
			set
			{
				_springJoint = value;
				enabled = lineRenderer.enabled = _springJoint != null;
			}
		}

		private LineRenderer _lineRenderer;
		public LineRenderer lineRenderer
		{
			get
			{
				if (!_lineRenderer)
					_lineRenderer = GetComponent<LineRenderer>();
				return _lineRenderer;
			}
		}

		Vector3[] positions = new Vector3[2];

		void Awake ()
		{
			lineRenderer.useWorldSpace = true;

			enabled = lineRenderer.enabled = springJoint != null;
		}

		void Update ()
		{
			positions[0] = springJoint.connectedBody != null ? springJoint.connectedBody.transform.TransformPoint(springJoint.connectedAnchor) : springJoint.connectedAnchor;
			positions[1] = springJoint.transform.TransformPoint(springJoint.anchor);
			lineRenderer.SetPositions(positions);
		}

		void Reset ()
		{
			springJoint = GetComponent<SpringJoint>();
		}

		[ContextMenu ("Initialise")]
		void Init ()
		{
			positions[0] = springJoint.connectedBody != null ? springJoint.connectedBody.transform.TransformPoint(springJoint.connectedAnchor) : springJoint.connectedAnchor;
			positions[1] = springJoint.transform.TransformPoint(springJoint.anchor);
			GetComponent<LineRenderer>().SetPositions(positions);
			GetComponent<LineRenderer>().useWorldSpace = true;
		}

		[ContextMenu ("Initialise", true)]
		bool Init_Validate ()
		{
			return (springJoint != null);
		}
	}
}