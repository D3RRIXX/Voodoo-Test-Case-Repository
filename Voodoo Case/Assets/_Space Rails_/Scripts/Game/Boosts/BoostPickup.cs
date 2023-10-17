using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Boosts
{
	public enum BoostType
	{
		None,
		Magnet,
		Shield,
		LavaBoots,
		IronPole
	}
	
	public class BoostPickup : PickupBase
	{
		[SerializeField] private BoostType _boostType;
		[SerializeField] private float _duration;
		
		private PlayerBoostHandler _boostHandler;

		[Inject]
		private void Construct(PlayerBoostHandler boostHandler)
		{
			_boostHandler = boostHandler;
		}
		
		public override void OnPickup(GameObject instigator)
		{
			_boostHandler.AddBoost(_boostType, _duration);
			
			base.OnPickup(instigator);
		}
	}
}