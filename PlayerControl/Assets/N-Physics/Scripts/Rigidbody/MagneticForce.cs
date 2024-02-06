//
//  MagneticForce.cs
//
//  Author:
//       Frederic Moreau <info@unitycoach.ca>
//
//  Copyright (c) 2018 Frederic Moreau, UnityCoach (Jikkou Publishing Inc.)

using UnityEngine;
using System.Linq;

// TODO : filter non magnetic rigidbodies
// TODO : add tooltips

namespace NPhysics
{
	/// <summary>
	/// Attracts the rigidbody to other rigid bodies, weighted by the masses
	/// </summary>
	[AddComponentMenu("N-Physics/Rigidbody/Magnetic Force")]
	[HelpURL ("http://unitycoach.ca/n-physics/")]
	[RequireComponent(typeof(Rigidbody))]
	public class MagneticForce : MonoBehaviour
	{
		[SerializeField] Rigidbody _attractor;

		Rigidbody [] _attractors;

		[SerializeField] float _strength = 10;
		[SerializeField] bool _useMasses;

		[SerializeField] bool _inverseSquare;

		[SerializeField] [Range(0.0f, 1000f)] float _range = 10;
		[SerializeField] AnimationCurve _curve = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 1, 0, -4), new Keyframe(1, 0, 0, 0) });

		Rigidbody _rigidbody;
		Vector3 _force;

		void Awake ()
		{
			_rigidbody = GetComponent<Rigidbody>();

			if (!_attractor)
				_attractors = (from b in FindObjectsOfType<Rigidbody>()
					where b != _rigidbody
					select b).ToArray();
			else
				_attractors = new Rigidbody[1] { _attractor };

			enabled = _attractors.Length != 0;
		}

		void FixedUpdate ()
		{
            if (!Physics.autoSimulation)
                return;

			for (int i = 0; i < _attractors.Length; i++)
			{
				_force = (_attractors[i].worldCenterOfMass - _rigidbody.worldCenterOfMass).normalized * _strength;

				if (_useMasses)
					_force *= _attractors[i].mass * _rigidbody.mass;

				if (_range > 0)
					_force *= _curve.Evaluate(Mathf.InverseLerp(0, _range, Vector3.Distance(_attractors[i].worldCenterOfMass, _rigidbody.worldCenterOfMass)));

				if (_inverseSquare)
					_force *= 1f / Mathf.Pow(Vector3.Distance(_attractors[i].worldCenterOfMass, _rigidbody.worldCenterOfMass), 2);

				_rigidbody.AddForce(_force, ForceMode.Force);
			}
		}
	}
}