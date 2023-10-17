using SpaceRails.Game.Boosts;
using SpaceRails.Game.Player;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Obstacles
{
	public class Landmine : Obstacle
	{
		[SerializeField] private ParticleSystem _vfxPrefab;
		
		private PlayerPoleHandler _playerPoleHandler;
		private PlayerBoostHandler _playerBoostHandler;

		[Inject]
		private void Construct(PlayerPoleHandler playerPoleHandler, PlayerBoostHandler playerBoostHandler)
		{
			_playerBoostHandler = playerBoostHandler;
			_playerPoleHandler = playerPoleHandler;
		}
		
		protected override void OnTouchedPlayer(Collision other)
		{
			PlayVFX();
			
			if (_playerBoostHandler.HasBoost(BoostType.Shield))
			{
				_playerBoostHandler.RemoveBoost(BoostType.Shield);
				return;
			}
		
			_playerPoleHandler.OnTouchedMine();
		}

		private void PlayVFX()
		{
			Instantiate(_vfxPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}