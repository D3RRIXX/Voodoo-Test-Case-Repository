using SpaceRails.Game.Player;
using UnityEngine;

namespace SpaceRails.Game.Boosts
{
	public class ShieldBoost : IBoost
	{
		[System.Serializable]
		public class Settings
		{
			public GameObject GfxPrefab;
		}

		private readonly Settings _settings;
		private readonly PlayerPoleHandler _playerPoleHandler;
		private GameObject _shieldGfx;

		public ShieldBoost(Settings settings, PlayerPoleHandler playerPoleHandler)
		{
			_settings = settings;
			_playerPoleHandler = playerPoleHandler;
		}

		public void Apply()
		{
			_shieldGfx = Object.Instantiate(_settings.GfxPrefab, _playerPoleHandler.transform, false);
		}

		public void Dispose()
		{
			Object.Destroy(_shieldGfx);
		}
	}
}