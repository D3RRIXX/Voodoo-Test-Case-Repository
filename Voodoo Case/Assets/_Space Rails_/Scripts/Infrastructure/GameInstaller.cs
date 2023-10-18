using SpaceRails.Game;
using SpaceRails.Game.Obstacles;
using SpaceRails.Game.Player;
using UnityEngine;
using Zenject;

namespace SpaceRails.Infrastructure
{
	[CreateAssetMenu(fileName = "Game Installer", menuName = SOPaths.INSTALLERS + "Game Installer", order = -100)]
	public class GameInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private Pole.Settings _poleSettings;
		[SerializeField] private LavaFloor.Settings _lavaFloorSettings;
		
		public override void InstallBindings()
		{
			Application.targetFrameRate = 60;
			SignalBusInstaller.Install(Container);

			Container.DeclareSignal<PlayerPoleLengthChangedSignal>();

			Container.BindInstance(_poleSettings);
			Container.BindInstance(_lavaFloorSettings);
			
			Container.Bind<GameStateManager>().AsSingle();
			Container.Bind<CurrencyManager>().AsSingle();
		}
	}
}