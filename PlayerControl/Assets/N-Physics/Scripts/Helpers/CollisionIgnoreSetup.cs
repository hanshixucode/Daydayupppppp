using UnityEngine;

namespace NPhysics.Helpers
{
	[AddComponentMenu ("N-Physics/Helpers/Collision Ignore Setup")]
	public class CollisionIgnoreSetup : MonoBehaviour
	{
		[System.Serializable]
		public class CollisionIgnorance
		{
			public Collider[] group1;
			public Collider[] group2;
		}

		[SerializeField] bool _childrenIgnoreParentsAndSiblings;
		[SerializeField] CollisionIgnorance[] _collisionIgnorances;

		void Awake ()
		{
			if (_childrenIgnoreParentsAndSiblings)
			{
				Collider[] hierarchyColliders = transform.root.GetComponentsInChildren<Collider>();
				Collider[] childrenColliders = GetComponentsInChildren<Collider>();
				foreach (Collider h in hierarchyColliders)
					foreach (Collider c in childrenColliders)
						Physics.IgnoreCollision(h, c);
			}
			
			foreach (CollisionIgnorance c in _collisionIgnorances)
				foreach (Collider c1 in c.group1)
					foreach (Collider c2 in c.group2)
						Physics.IgnoreCollision(c1, c2);
		}
	}
}