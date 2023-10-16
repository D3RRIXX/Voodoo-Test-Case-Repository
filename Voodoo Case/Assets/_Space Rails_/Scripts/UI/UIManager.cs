using System.Collections.Generic;
using System.Linq;
using SpaceRails.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceRails.UI
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private UIScreen[] _uiScreens;

		private Dictionary<GameState, UIScreen> _uiScreenMap;
		
		[Inject]
		private void Construct(GameStateManager gameStateManager)
		{
			_uiScreenMap = _uiScreens.ToDictionary(x => x.GameState, x => x);
			
			gameStateManager.CurrentState
			                .Subscribe(SwitchUIScreen)
			                .AddTo(this);

			gameStateManager.CurrentState.Value = GameState.Menu;
		}

		private void SwitchUIScreen(GameState gameState)
		{
			if (!_uiScreenMap.ContainsKey(gameState))
				return;

			foreach (UIScreen uiScreen in _uiScreenMap.Values)
			{
				uiScreen.gameObject.SetActive(uiScreen.GameState == gameState);
			}
		}
	}
}