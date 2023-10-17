using System;
using SpaceRails.Game.Player;
using UnityEngine;

namespace SpaceRails.Game.Boosts
{
	public class IronPoleBoost : IBoost
	{
		[Serializable]
		public class Settings
		{
			public Material MetalMaterial;
		}

		private readonly Settings _settings;
		private readonly Material _cachedMaterial;
		private readonly MeshRenderer _poleRenderer;

		public IronPoleBoost(Settings settings, PlayerPoleHandler playerPoleHandler)
		{
			_settings = settings;
			_poleRenderer = playerPoleHandler.Pole.GetComponent<MeshRenderer>();
			_cachedMaterial = _poleRenderer.material;
		}

		public void Apply()
		{
			_poleRenderer.material = _settings.MetalMaterial;
		}

		public void Dispose()
		{
			_poleRenderer.material = _cachedMaterial;
		}
	}
}