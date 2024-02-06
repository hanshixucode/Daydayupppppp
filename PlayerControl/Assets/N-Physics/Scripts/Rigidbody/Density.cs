using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
#endif

namespace NPhysics
{
	[AddComponentMenu("N-Physics/Rigidbody/Density")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[DisallowMultipleComponent]
	[RequireComponent (typeof (Rigidbody))]
	public class Density : MonoBehaviour
	{
		[SerializeField] float _density;
		public float density
		{
			get { return _density; }
			private set
			{
				if (value != _density)
					return;
				
				_density = value;

				rigidBody.SetDensity(_density);
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

		void GuessDensity ()
		{
			float mass = rigidBody.mass;
			rigidBody.SetDensity(1);
			_density = mass/rigidBody.mass;
			rigidBody.SetDensity(_density);
		}

		[ContextMenu ("Set Density")]
		void ApplyDensity ()
		{
			rigidBody.SetDensity(_density);
		}

		#if UNITY_EDITOR
		void OnValidate ()
		{
			// updates rigid body's mass when value changes
			rigidBody.SetDensity(_density);
			// forcing inspector refresh through Reflection
			rigidBody.GetType().GetProperty("mass").SetValue(rigidBody, rigidBody.mass, null);
		}
		#endif

		void Reset ()
		{
			GuessDensity();
		}
	}
}