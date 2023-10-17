using UnityEngine;
using Zenject;

namespace SpaceRails.Infrastructure
{
	public class AudioSystemInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<AudioSource>().FromInstance(GetComponentInChildren<AudioSource>()).AsSingle().WhenInjectedInto<AudioPlayer>();
			Container.Bind<AudioPlayer>().AsSingle();
		}
	}
}