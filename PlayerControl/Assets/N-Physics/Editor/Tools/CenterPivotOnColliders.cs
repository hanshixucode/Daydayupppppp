//
//  CenterPivotOnColliders.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using UnityEditor;

namespace NPhysics.Editor
{
	public static class CenterPivotOnColliders
	{
		[MenuItem ("N-Physics/Center Transform on Colliders Bounds")]
		public static void CenterPivotOnCollidersBounds ()
		{
			Transform tx = Selection.activeTransform;
			Collider[] colliders = tx.GetComponentsInChildren<Collider>();
			BoxCollider[] bColliders = tx.GetComponentsInChildren<BoxCollider>();
			SphereCollider[] sColliders = tx.GetComponentsInChildren<SphereCollider>();
			CapsuleCollider[] cColliders = tx.GetComponentsInChildren<CapsuleCollider>();

			Vector3 position = Vector3.zero;
			foreach (Collider col in colliders)
			{
				position += col.bounds.center;
			}
			position /= colliders.Length;
			Vector3 offset = position - tx.position;

			int undo = Undo.GetCurrentGroup();

			foreach (BoxCollider col in bColliders)
			{
				Undo.RecordObject(col, "");
				col.center -= offset;
			}
			foreach (SphereCollider col in sColliders)
			{
				Undo.RecordObject(col, "");
				col.center -= offset;
			}
			foreach (CapsuleCollider col in cColliders)
			{
				Undo.RecordObject(col, "");
				col.center -= offset;
			}

			Undo.RecordObject(tx, "");
			tx.position += offset;
			Undo.SetCurrentGroupName("Center Transform on Colliders Bounds");
			Undo.CollapseUndoOperations(undo);
		}

		[MenuItem ("N-Physics/Center Transform on Colliders Bounds", true)]
		public static bool CenterPivotOnCollidersBounds_Validate ()
		{
			return Selection.activeTransform && Selection.activeTransform.GetComponentsInChildren<Collider>().Length != 0;
		}
	}
}