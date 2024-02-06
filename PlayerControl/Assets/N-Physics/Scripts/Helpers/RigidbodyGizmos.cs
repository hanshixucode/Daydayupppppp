using UnityEngine;

namespace NPhysics.Helpers
{
    /// <summary>
    /// Helper Component to display Rigidbodies' values.
    /// </summary>
	[AddComponentMenu("N-Physics/Helpers/Rigidbody Gizmos")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
    public class RigidbodyGizmos : MonoBehaviour
    {
        [SerializeField] bool _displayCenterOfMass = true;
        [SerializeField] bool _displayInertiaTensor;
        [SerializeField] bool _displayVelocityVector;
		[SerializeField] Color _centerOfMassColor = Color.yellow;
		[SerializeField] Color _centerOfMassColorWhenAsleep = Color.black;
		[SerializeField] Color _velocityColor = Color.cyan;

		Vector3 _min, _max;
		private float _size = 0;
		public float size
		{
			get
			{
				if (_size == 0)
				{
					Collider[] colliders = GetComponentsInChildren<Collider>();
					if (colliders.Length != 0)
					{
						for (int i = 0 ; i < colliders.Length ; i++)
						{
							_min = Vector3.Min(colliders[i].bounds.min, _min);
							_max = Vector3.Max(colliders[i].bounds.max, _max);
						}
						_size = (_max - _min).magnitude * 0.01f;
					}
					else
					{
						_size = 0.01f;
					}
				}
				return _size;
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

		void Reset ()
		{
			_size = 0;
		}

		[ContextMenu ("Reset Center of Mass")]
		void ResetCenterOfMass ()
		{
			rigidBody.ResetCenterOfMass();
		}

		[ContextMenu ("Reset Inertia Tensor")]
		void ResetInertiaTensor ()
		{
			rigidBody.ResetInertiaTensor();
		}

        void OnDrawGizmos()
        {
			if (!enabled)
				return;
			
            if (_displayCenterOfMass)
            {
				Gizmos.color = rigidBody.IsSleeping() ? _centerOfMassColorWhenAsleep : _centerOfMassColor;
				Gizmos.DrawWireSphere(rigidBody.worldCenterOfMass, size);
            }

            if (_displayVelocityVector)
            {
				Gizmos.color = _velocityColor;
                Gizmos.DrawRay(rigidBody.worldCenterOfMass, rigidBody.velocity);
            }

            if (_displayInertiaTensor)
            {
                Gizmos.color = Color.red;
				Gizmos.DrawRay(rigidBody.worldCenterOfMass, (rigidBody.inertiaTensorRotation * rigidBody.transform.right) * rigidBody.inertiaTensor.x);
                
                Gizmos.color = Color.green;
				Gizmos.DrawRay(rigidBody.worldCenterOfMass, (rigidBody.inertiaTensorRotation * rigidBody.transform.up) * rigidBody.inertiaTensor.y);
                
                Gizmos.color = Color.blue;
				Gizmos.DrawRay(rigidBody.worldCenterOfMass, (rigidBody.inertiaTensorRotation * rigidBody.transform.forward) * rigidBody.inertiaTensor.z);
            }
        }
    }
}