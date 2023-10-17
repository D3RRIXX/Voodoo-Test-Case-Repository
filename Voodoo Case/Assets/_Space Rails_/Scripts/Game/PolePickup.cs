using SpaceRails.Game.Player;
using UnityEngine;

namespace SpaceRails.Game
{
	public class PolePickup : PickupBase
	{
		[SerializeField, Min(1)] private int _poleGain;
		[SerializeField] private Pole _pole;

		private void Reset()
		{
			_pole = GetComponentInChildren<Pole>();
		}

		private void Start()
		{
			_pole.Segments = _poleGain;
		}

		public override void OnPickup(GameObject instigator)
		{
			instigator.GetComponent<PlayerPoleHandler>().Pole.AddSegments(_poleGain);
			base.OnPickup(instigator);
		}
	}
}