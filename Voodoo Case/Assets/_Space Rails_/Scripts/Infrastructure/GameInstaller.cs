using SpaceRails.Game;
using SpaceRails.Game.Obstacles;
using SpaceRails.Game.Player;
using UnityEngine;
using Zenject;

namespace SpaceRails.Infrastructure
{
	[CreateAssetMenu(fileName = "Game Installer", menuName = "Space Rails/Game Installer", order = -100)]
	public class GameInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private Pole.Settings _poleSettings;
		[SerializeField] private LavaFloor.Settings _lavaFloorSettings;
		[SerializeField] private PrefabContainer _prefabContainer;
		
		public override void InstallBindings()
		{
			SignalBusInstaller.Install(Container);

			Container.BindInstance(_poleSettings);
			Container.BindInstance(_lavaFloorSettings);
			Container.BindInstance(_prefabContainer);
			
			Container.Bind<GameStateManager>().AsSingle();
			Container.Bind<CurrencyManager>().AsSingle();
		}
	}
}