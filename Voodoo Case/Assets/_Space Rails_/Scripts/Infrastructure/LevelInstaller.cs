using SpaceRails.Game;
using SpaceRails.Game.Player;
using SpaceRails.Game.Signals;
using SpaceRails.UI;
using UnityEngine;
using Zenject;

namespace SpaceRails.Infrastructure
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private PoleSegmentGainPopup _popupPrefab;
		
		public override void InstallBindings()
		{
			Container.DeclareSignal<PlayerInvincibilityStartedSignal>();
			Container.DeclareSignal<PlayerInvincibilityEndedSignal>();
			
			Container.Bind<PlayerPoleHandler>().FromComponentInHierarchy().AsSingle();

			Container.BindFactory<string, PoleSegmentGainPopup, PoleSegmentGainPopup.Factory>()
			         .FromMonoPoolableMemoryPool(x => x.FromComponentInNewPrefab(_popupPrefab));
		}
	}
}