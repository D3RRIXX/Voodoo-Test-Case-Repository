using UnityEngine;

namespace SpaceRails.Game
{
	public class PolePickup : MonoBehaviour, IPickup
	{
		[SerializeField] private int _poleGain;
		
		public void OnPickup(GameObject instigator)
		{
			instigator.GetComponent<PoleController>().PoleLength += _poleGain;
			Destroy(gameObject);
		}
	}
}