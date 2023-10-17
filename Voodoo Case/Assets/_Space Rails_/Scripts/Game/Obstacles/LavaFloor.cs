using System;
using SpaceRails.Game.Boosts;
using SpaceRails.Game.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Obstacles
{
	public class LavaFloor : MonoBehaviour
	{
		[Serializable]
		public class Settings
		{
			public float DamageInterval = 0.15f;
			public float DamageAmount = 0.1f;
		}

		private Settings _settings;
		private IDisposable _damagePlayer;
		private PlayerBoostHandler _playerBoostHandler;

		[Inject]
		private void Construct(Settings settings, PlayerBoostHandler playerBoostHandler)
		{
			_playerBoostHandler = playerBoostHandler;
			_settings = settings;
		}
		
		private void Awake()
		{
			this.OnTriggerEnterAsObservable()
			    .Where(IsPlayer)
			    .Select(x => x.GetComponent<PlayerPoleHandler>())
			    .Subscribe(StartDamagingPlayer)
			    .AddTo(this);

			this.OnTriggerExitAsObservable()
			    .Where(IsPlayer)
			    .Subscribe(_ =>
			    {
				    _damagePlayer.Dispose();
				    _damagePlayer = null;
			    })
			    .AddTo(this);
		}

		private void OnDestroy()
		{
			_damagePlayer?.Dispose();
		}

		private void StartDamagingPlayer(PlayerPoleHandler playerPoleHandler)
		{
			_damagePlayer = Observable.Interval(TimeSpan.FromSeconds(_settings.DamageInterval))
			                          .StartWith(0)
			                          .Where(_ => !_playerBoostHandler.HasBoost(BoostType.LavaBoots))
			                          .Subscribe(_ => playerPoleHandler.Pole.Length -= _settings.DamageAmount);
		}

		private static bool IsPlayer(Collider collider) => collider.CompareTag("Player");
	}
}