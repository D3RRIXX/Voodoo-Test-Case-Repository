using UnityEngine;

namespace SpaceRails.Game.Obstacles
{
	[RequireComponent(typeof(Collider))]
	public abstract class Obstacle : MonoBehaviour
	{
		protected Collider Collider { get; private set; }

		private void Awake()
		{
			Collider = GetComponent<Collider>();
		}

		private void OnCollisionEnter(Collision other)
		{
			if (!other.gameObject.CompareTag("Player"))
				return;

			OnTouchedPlayer(other);
		}

		protected abstract void OnTouchedPlayer(Collision other);
	}
}