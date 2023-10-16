using UnityEngine;

namespace SpaceRails.Game
{
	public class PolePickup : MonoBehaviour, IPickup
	{
		[SerializeField, Min(1)] private int _poleGain;
		[SerializeField] private Pole _pole;

		private void Reset()
		{
			_pole = GetComponentInChildren<Pole>();
		}

		private void Start()
		{
			_pole.Length = _poleGain;
		}

		public void OnPickup(GameObject instigator)
		{
			instigator.GetComponent<PlayerPoleHandler>().Pole.AddSegments(_poleGain);
			Destroy(gameObject);
		}
	}
}