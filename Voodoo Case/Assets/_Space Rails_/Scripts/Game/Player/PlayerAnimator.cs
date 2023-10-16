using System;
using SpaceRails.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Player
{
	[RequireComponent(typeof(Animator))]
	public class PlayerAnimator : MonoBehaviour
	{
		private PlayerMovement _playerMovement;
		private Animator _animator;
		
		private static readonly int HANGING = Animator.StringToHash("Hanging");
		private static readonly Vector3 HANGING_OFFSET = new Vector3(0f, -0.65f, 0f);
		private static readonly int START = Animator.StringToHash("Start");

		[Inject]
		private void Construct(GameStateManager gameStateManager)
		{
			gameStateManager.CurrentState
			                .First(x => x is GameState.Game)
			                .Subscribe(_ => _animator.SetTrigger(START))
			                .AddTo(this);
		}
		
		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_playerMovement = GetComponentInParent<PlayerMovement>();
			_playerMovement.MovementType.Subscribe(OnMovementTypeChanged);
		}

		private void OnMovementTypeChanged(MovementType movementType)
		{
			switch (movementType)
			{
				case MovementType.OnFoot:
					_animator.SetBool(HANGING, false);
					_animator.transform.localPosition = Vector3.zero;
					break;
				case MovementType.Rails:
					_animator.SetBool(HANGING, true);
					_animator.transform.localPosition = HANGING_OFFSET;
					break;
				case MovementType.FreeFall:
					break;
			}
		}
	}
}