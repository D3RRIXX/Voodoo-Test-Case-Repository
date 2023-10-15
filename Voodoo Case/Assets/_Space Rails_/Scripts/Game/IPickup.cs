using UnityEngine;

namespace SpaceRails.Game
{
	public interface IPickup
	{
		void OnPickup(GameObject instigator);
	}
}