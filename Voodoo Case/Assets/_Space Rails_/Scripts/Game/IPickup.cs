using UnityEngine;

namespace SpaceRails.Game
{
	public interface IPickup
	{
		void OnPickup(GameObject instigator);
	}

	[RequireComponent(typeof(Collider))]
	public abstract class PickupBase : MonoBehaviour, IPickup
	{
		public virtual void OnPickup(GameObject instigator)
		{
			Destroy(gameObject);
		}
	}
}