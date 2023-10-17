using System.Collections.Generic;
using SpaceRails.Game.Player;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Boosts
{
	public class MagnetBoost : ITickable, IBoost
	{
		[System.Serializable]
		public class Settings
		{
			public float Radius = 10f;
			public float MagnetPower = 10f;
		}
		
		private static readonly Collider[] OVERLAP_RESULTS = new Collider[50];
		
		private readonly Settings _settings;
		private readonly PlayerPoleHandler _poleHandler;
		private readonly TickableManager _tickableManager;

		public MagnetBoost(Settings settings, TickableManager tickableManager, PlayerPoleHandler poleHandler)
		{
			_tickableManager = tickableManager;
			_settings = settings;
			_poleHandler = poleHandler;
		}

		public void Apply()
		{
			_tickableManager.Add(this);
		}

		public void Dispose()
		{
			_tickableManager.Remove(this);
		}

		public void Tick()
		{
			Vector3 halfHeight = Vector3.up;
			Vector3 playerPos = _poleHandler.transform.position + halfHeight;
			
			int hitCount = Physics.OverlapSphereNonAlloc(playerPos, _settings.Radius, OVERLAP_RESULTS);
			float distanceDelta = _settings.MagnetPower * Time.deltaTime;
			
			for (int i = 0; i < hitCount; i++)
			{
				if (OVERLAP_RESULTS[i].TryGetComponent(out CoinPickup coinPickup))
				{
					coinPickup.transform.position = Vector3.MoveTowards(coinPickup.transform.position, playerPos, distanceDelta);
				}
			}
		}
	}
}