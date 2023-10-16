using System;
using System.Threading.Tasks;
using DG.Tweening;
using SpaceRails.Game.Signals;
using SpaceRails.Utilities;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
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

		public Pole Pole => _pole;
		public bool IsInvincible { get; private set; }

		[Inject]
		private void Construct(SignalBus signalBus)
		{
			_signalBus = signalBus;
		}
		
		private void Awake()
		{
			_playerMovement = GetComponent<PlayerMovement>();
		}

		public void LosePole()
		{
			if (_lostPole)
				return;
			
			Debug.Log("Lost pole!");
			_lostPole = true;
			Pole.transform.SetParent(null);
			Pole.gameObject.AddComponent<Rigidbody>();

			_playerMovement.ChangeMovementType(MovementType.FreeFall);
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