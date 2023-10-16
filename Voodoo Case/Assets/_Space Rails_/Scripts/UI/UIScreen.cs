using SpaceRails.Infrastructure;
using UnityEngine;

namespace SpaceRails.UI
{
	public class UIScreen : MonoBehaviour
	{
		[SerializeField] private GameState _gameState;

		public GameState GameState => _gameState;
	}
}