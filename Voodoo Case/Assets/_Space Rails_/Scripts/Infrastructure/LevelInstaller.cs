using SpaceRails.Game;
using UnityEngine;
using Zenject;

namespace SpaceRails.Infrastructure
{
	public class LevelInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<PlayerPoleHandler>().FromComponentInHierarchy().AsSingle();
		}
	}
}