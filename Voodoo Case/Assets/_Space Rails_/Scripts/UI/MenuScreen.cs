using SpaceRails.Infrastructure;
using UnityEngine;
using Zenject;

namespace SpaceRails.UI
{
	public class MenuScreen : MonoBehaviour
	{
		private GameStateManager _gameStateManager;

		[Inject]
		private void Construct(GameStateManager gameStateManager)
		{
			_gameStateManager = gameStateManager;
		}
		
		public void StartGame()
		{
			_gameStateManager.CurrentState.Value = GameState.Game;
		}
	}
}