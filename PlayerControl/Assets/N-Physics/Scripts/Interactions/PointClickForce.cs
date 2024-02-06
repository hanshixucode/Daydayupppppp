using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace NPhysics
{
	/// <summary>
	/// PointClickForce.
	/// Helper class to test Physics Forces behaviours on Rigid bodies
	/// </summary>
	[AddComponentMenu ("N-Physics/Simulation/Point Click Force")]
	public class PointClickForce : MonoBehaviour
	{
		#region enums & events
		public enum ForceType {Force, ForceAtPosition, ExplosionForce, InstanciateExplosion};
		public enum Direction {View, Normal, Up, Right, Forward};
		public enum RayMode {MousePositionToRay, TransformForward};
		#endregion

		#region inspector fields
		[Tooltip ("Three types of forces can be applied to rigid bodies :\n" +
			"Force : applies force to the ridid body at its center of mass.\n" +
			"Force At Position : applies force to the rigid body at given position. Results in Force and Torque combined.\n" +
			"Explosion Force : similar to Force at Position, with an extra uplift simulating shock wave on surroundings.\n" +
			"Instanciate Explosion : instanciates an Explosion prefab object. (To be found in Standard Assets/Particles/Prefabs.")]
		[SerializeField] ForceType _forceType = ForceType.ForceAtPosition;
		float _defaultForce;

		[Tooltip ("Amount of force to be applied, in Newtons.")]
		[SerializeField] float _force = 1;

		[Tooltip ("Force modes yield different behaviours.\n" +
			"Force : Add a continuous force to the rigidbody, using its mass.\n" +
			"Acceleration : Add a continuous acceleration to the rigidbody, ignoring its mass.\n" +
			"Impulse : Add an instant force impulse to the rigidbody, using its mass.\n" +
			"VelocityChange : Add an instant velocity change to the rigidbody, ignoring its mass.")]
		[SerializeField] ForceMode _forceMode = ForceMode.Impulse;

		[Tooltip ("Force direction")]
		[SerializeField] Direction _direction = Direction.Normal;

		[Tooltip ("Inverse force direction")]
		[SerializeField] bool _inverse;

		[Tooltip ("Explosion Effect Range. Rigidbodies outside this range won't be affected.")]
		[SerializeField] float _explosionRange = 10f;

		[Tooltip ("Adjustment to the apparent position of the explosion to make it seem to lift objects.")]
		[SerializeField] float _explosionUplift = 10f;

		[Tooltip ("Pointer object. Either Prefab or Scene Object")]
		[SerializeField] GameObject locator;

		[Tooltip ("Explosion object. Use Explosion Prefabs from Standard Assets.")]
		[SerializeField] GameObject explosionPrefab;

		[Tooltip ("MousePositionToRay : cast a ray from Camera through mouse screen position.\n" +
			"TransformForward : cast a ray in this gameobject's forward direction.")]
		[SerializeField] RayMode _rayMode;

		[Tooltip ("Layers to Raycast against.")]
		[SerializeField] LayerMask _rayCastMask = -5;

		[Tooltip ("Raycast Max. Distance.")]
		[SerializeField] float _maxDistance = 100;
		#endregion

		#region accessors
		public ForceType forceType
		{
			get { return _forceType; }
			set { _forceType = value; }
		}
		public void SetForceType (int type)
		{
			forceType = (ForceType)type;
		}
		public float force
		{
			get { return _force; }
			set
			{
				_force = value;
			}
		}
		public ForceMode forceMode
		{
			get { return _forceMode; }
			set { _forceMode = value; }
		}
		public void SetForceMode (int mode)
		{
			if (mode == 3)
				forceMode = ForceMode.Acceleration; // HACK : ForceMode.Acceleration = 5, not 3 
			else
				forceMode = (ForceMode)mode;
		}
		public Direction direction
		{
			get { return _direction; }
			set { _direction = value; }
		}
		public void SetDirection (int dir)
		{
			direction = (Direction)dir;
		}
		public bool inverse
		{
			get { return _inverse; }
			set { _inverse = value; }
		}
		public float explosionRange
		{
			get { return _explosionRange; }
			set { _explosionRange = value; }
		}
		public float explosionUplift
		{
			get { return _explosionUplift; }
			set { _explosionUplift = value; }
		}

		private bool _hovering;
		public bool hovering
		{
			get { return _hovering; }
			private set
			{
				if (value != _hovering)
				{
					_hovering = value;
					locator.SetActive(_hovering);
				}
			}
		}
		#endregion

		#region private fields
		Camera _mainCamera;
		Rigidbody[] rigidbodies;

		public bool mouseButtonDown {get; set;}
		public bool mouseButtonUp {get; set;}

		RaycastHit _rayCastHit;
		Vector3 forceDirection = Vector3.zero;
		Vector3 xzPlaneVector = new Vector3 (1, 0, 1);
		Vector3 _forceVector;
		Ray ray;
		#endregion

		#region MonoBeheviour messages
		void Awake ()
		{
			rigidbodies = FindObjectsOfType<Rigidbody>();
			if (locator.scene.name == null)
				locator = Instantiate(locator, transform);
		}

		void Start ()
		{
			locator.SetActive(false);
			_mainCamera = Camera.main;
			_defaultForce = force;
		}

		void Update ()
		{
			if (_rayMode != RayMode.MousePositionToRay)
				return;

			mouseButtonDown = (Input.GetMouseButton(0) && (!EventSystem.current || !EventSystem.current.IsPointerOverGameObject()));
			if (Input.GetMouseButtonUp(0) && (!EventSystem.current || !EventSystem.current.IsPointerOverGameObject()))
				mouseButtonUp = true;
		}

		void FixedUpdate ()
		{
			if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
			{
				hovering = false;
				return;
			}

			if (_rayMode == RayMode.MousePositionToRay)
			{
				ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			}
			else
			{
				ray.origin = transform.position;
				ray.direction = transform.forward;
			}

			hovering = Physics.Raycast(ray, out _rayCastHit, _maxDistance, _rayCastMask, QueryTriggerInteraction.Ignore);
			hovering &= (forceType == ForceType.ExplosionForce || forceType == ForceType.InstanciateExplosion || _rayCastHit.rigidbody != null);

			if (hovering)
			{
				Debug.DrawLine (ray.origin, _rayCastHit.point, Color.gray);
				Debug.DrawRay (_rayCastHit.point, _rayCastHit.normal, Color.yellow);

				switch (direction)
				{
					case Direction.View:
						forceDirection = (_inverse ? -ray.direction : ray.direction);
						break;
					case Direction.Normal:
						forceDirection = (_inverse ? _rayCastHit.normal : -_rayCastHit.normal);
						break;
					case Direction.Up:
						forceDirection = (_inverse ? Vector3.down : Vector3.up);
						break;
					case Direction.Right:
						forceDirection = (_inverse ? -_mainCamera.transform.right : _mainCamera.transform.right);
						forceDirection.Scale(xzPlaneVector);
						forceDirection.Normalize();
						break;
					case Direction.Forward:
						forceDirection = (_inverse ? -_mainCamera.transform.forward : _mainCamera.transform.forward);
						forceDirection.Scale(xzPlaneVector);
						forceDirection.Normalize();
						break;
				}

				locator.transform.SetPositionAndRotation(_rayCastHit.point, Quaternion.FromToRotation(Vector3.up, -forceDirection));

				if (mouseButtonDown)
				{
					if (forceMode == ForceMode.Force || forceMode == ForceMode.Acceleration)
						TriggerForce();
				}
				else if (mouseButtonUp)
				{
					if (forceType == ForceType.ExplosionForce)
						TriggerExplosion();
					else if (forceType == ForceType.InstanciateExplosion && explosionPrefab != null)
						Instantiate(explosionPrefab, _rayCastHit.point, Quaternion.FromToRotation(Vector3.up, -forceDirection));
					else if (forceMode == ForceMode.Impulse || forceMode == ForceMode.VelocityChange)
						TriggerForce();
				}
			}
			mouseButtonUp = false;
		}
		#endregion

		#region custom methods
		void TriggerForce ()
		{
			_forceVector = forceDirection * force;

			Debug.DrawRay(_rayCastHit.point, _forceVector, Color.cyan);

			switch (forceType)
			{
				case ForceType.Force:
					_rayCastHit.rigidbody.AddForce(_forceVector, forceMode);
					break;
				case ForceType.ForceAtPosition:
					_rayCastHit.rigidbody.AddForceAtPosition(_forceVector, _rayCastHit.point, forceMode);
					break;
				default:
					break;
			}
		}

		void TriggerExplosion ()
		{
			foreach (Rigidbody rb in rigidbodies)
			{
				float distance = Vector3.Distance(rb.position, _rayCastHit.point);
				float explosionForce = Mathf.Lerp(0, force, Mathf.InverseLerp(explosionRange, 0, distance));
				rb.AddExplosionForce(explosionForce, _rayCastHit.point, explosionRange, explosionUplift, forceMode);
			}
		}
		#endregion
	}
}