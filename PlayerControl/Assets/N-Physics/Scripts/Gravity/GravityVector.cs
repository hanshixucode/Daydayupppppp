//
//  GravityVector.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;

namespace NPhysics
{
    [AddComponentMenu("N-Physics/Gravity/Vector")]
    [HelpURL("http://unitycoach.ca/n-physics/")]
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class GravityVector : MonoBehaviour
    {
        public float gravityScale = 9.8f;

        void Update()
        {
            Physics.gravity = -transform.up * gravityScale;
        }

        void Reset()
        {
            transform.up = -Physics.gravity.normalized;
            gravityScale = Physics.gravity.magnitude;
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Gravity Vector", menuItem = "GameObject/N-Physics/Gravity Vector", priority = 11)]
        public static void CreateGravityVector()
        {
            GameObject o = new GameObject("Gravity Vector", new System.Type[] { typeof(Transform), typeof(GravityVector) });
            UnityEditor.Undo.RegisterCreatedObjectUndo(o, "Create Gravity Vector");
        }
#endif
    }
}