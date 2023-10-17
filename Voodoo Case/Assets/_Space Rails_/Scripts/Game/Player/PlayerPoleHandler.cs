using System;
using System.Threading.Tasks;
using DG.Tweening;
using SpaceRails.Game.Signals;
using SpaceRails.Infrastructure;
using SpaceRails.Utilities;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Player
{
	public class PlayerPoleHandler : MonoBehaviour
	{
		[SerializeField] private Pole _pole;

		[SerializeField] private float _restorePosDelay = 1f;
		[SerializeField] private float _tweenDuration = 0.5f;
		[SerializeField] private float _invincibilityDuration = 3f;

		private bool _lostPole;

		private Sequence _sequence;
		private PlayerMovement _playerMovement;
		private SignalBus _signalBus;
		private GameStateManager _gameStateManager;
		private Tween _tween;

		public Pole Pole => _pole;
		public bool IsInvincible { get; private set; }

		[Inject]
		private void Construct(SignalBus signalBus, GameStateManager gameStateManager)
		{
			_gameStateManager = gameStateManager;
			_signalBus = signalBus;

			_gameStateManager.CurrentState
			                 .First(x => x is GameState.LevelComplete)
			                 .Subscribe(_ => DropPole())
			                 .AddTo(this);
		}

		private void OnDestroy()
		{
			_tween.Kill();
			_sequence.Kill();
		}

		private void Awake()
		{
			_playerMovement = GetComponent<PlayerMovement>();

			_signalBus.GetStream<PlayerPoleLengthChangedSignal>()
			          .Where(x => x.Pole.Length <= 0f)
			          .Subscribe(_ => LoseLevel())
			          .AddTo(this);
		}

		public void LosePole()
		{
			if (_lostPole)
				return;

			_lostPole = true;
			_playerMovement.ChangeMovementType(MovementType.FreeFall);

			DropPole();

			_tween = DOVirtual.DelayedCall(1f, LoseLevel);
		}

		private void DropPole()
		{
			Pole.transform.SetParent(null);
			Pole.gameObject.AddComponent<Rigidbody>();
		}

		private void LoseLevel()
		{
			_gameStateManager.CurrentState.Value = GameState.LevelFailed;
		}

		public void CutOff(float length, Vector3 deltaPos)
		{
			_sequence.Kill();

			float newX = length * -Mathf.Sign(deltaPos.x);
			Pole.Length -= length;
			Pole.transform.localPosition = Pole.transform.localPosition.WithNewX(newX);

			_sequence = DOTween.Sequence()
			                   .AppendInterval(_restorePosDelay)
			                   .Append(Pole.transform.DOLocalMoveX(0f, _tweenDuration));
		}

		public async void OnTouchedMine()
		{
			if (IsInvincible)
				return;

			IsInvincible = true;
			Pole.Length /= 2f;

			_signalBus.Fire(new PlayerInvincibilityStartedSignal { Duration = _invincibilityDuration });

			await Task.Delay(TimeSpan.FromSeconds(_invincibilityDuration));

			IsInvincible = false;
		}
	}
}