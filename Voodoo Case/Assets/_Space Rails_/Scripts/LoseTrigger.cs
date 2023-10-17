using SpaceRails.Infrastructure;
using UnityEngine;
using Zenject;

namespace SpaceRails
{
	[RequireComponent(typeof(BoxCollider))]
	public class LoseTrigger : MonoBehaviour
	{
		private GameStateManager _gameStateManager;

		[Inject]
		private void Construct(GameStateManager gameStateManager)
		{
			_gameStateManager = gameStateManager;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				_gameStateManager.CurrentState.Value = GameState.LevelFailed;
		}
	}
}