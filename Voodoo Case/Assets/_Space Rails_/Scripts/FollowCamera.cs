using System;
using SpaceRails.Infrastructure;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace SpaceRails
{
	public class FollowCamera : MonoBehaviour
	{
		[SerializeField] private Transform _target;

		private Vector3 _offset;

		[Inject]
		private void Construct(GameStateManager gameStateManager)
		{
			_offset = transform.position - _target.position;

			this.LateUpdateAsObservable()
			    .TakeUntil(gameStateManager.CurrentState.SkipLatestValueOnSubscribe().First(x => x is GameState.LevelComplete or GameState.LevelFailed))
			    .Subscribe(_ => transform.position = _target.position + _offset);
		}
	}
}