using System;
using UnityEngine;

namespace SpaceRails.Game
{
	[RequireComponent(typeof(Collider))]
	public class CollisionEventSubject : MonoBehaviour
	{
		public event Action<Collision> CollisionEnter;

		private void OnCollisionEnter(Collision other)
		{
			CollisionEnter?.Invoke(other);
		}
	}
}