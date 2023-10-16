using UniRx;

namespace SpaceRails.Infrastructure
{
	public enum GameState
	{
		None = 0,
		Menu,
		Game,
		LevelComplete,
		LevelFailed
	}
	
	public class GameStateManager
	{
		public ReactiveProperty<GameState> CurrentState { get; } = new();
	}
}