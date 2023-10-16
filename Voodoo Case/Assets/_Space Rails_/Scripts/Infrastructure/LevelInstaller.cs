using SpaceRails.Game;
using SpaceRails.Game.Signals;
using UnityEngine;
using Zenject;

namespace SpaceRails.Infrastructure
{
	public class LevelInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.DeclareSignal<PlayerInvincibilityStartedSignal>();
			Container.DeclareSignal<PlayerInvincibilityEndedSignal>();
			
			Container.Bind<PlayerPoleHandler>().FromComponentInHierarchy().AsSingle();
		}
	}
}