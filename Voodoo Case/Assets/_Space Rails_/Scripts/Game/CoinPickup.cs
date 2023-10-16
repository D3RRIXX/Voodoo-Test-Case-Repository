using SpaceRails.Infrastructure;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public class CoinPickup : PickupBase
	{
		private CurrencyManager _currencyManager;

		[Inject]
		private void Construct(CurrencyManager currencyManager)
		{
			_currencyManager = currencyManager;
		}
		
		public override void OnPickup(GameObject instigator)
		{
			_currencyManager.Coins.Value++;
			base.OnPickup(instigator);
		}
	}
}