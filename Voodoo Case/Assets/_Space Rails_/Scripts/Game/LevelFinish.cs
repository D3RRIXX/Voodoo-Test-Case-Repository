using SpaceRails.Infrastructure;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public class LevelFinish : MonoBehaviour
	{
		private GameStateManager _gameStateManager;

		[Inject]
		private void Construct(GameStateManager gameStateManager)
		{
			_gameStateManager = gameStateManager;
		}
		
		private void OnTriggerEnter(Collider other)
		{
			_gameStateManager.CurrentState.Value = GameState.LevelComplete;
		}
	}
}