using SpaceRails.Infrastructure;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Boosts
{
	[CreateAssetMenu(fileName = "Boost System Installer", menuName = SOPaths.INSTALLERS + "Boost System")]
	public class BoostSystemInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private MagnetBoost.Settings _magnetSettings;
		
		public override void InstallBindings()
		{
			Container.BindInstance(_magnetSettings);

			BindBoostFactory<MagnetBoost>(BoostType.Magnet);
			BindBoostFactory<LavaBootsBoost>(BoostType.LavaBoots);
			
			Container.BindInterfacesAndSelfTo<PlayerBoostHandler>().AsSingle();
		}

		private void BindBoostFactory<TBoost>(BoostType boostType) where TBoost : IBoost
		{
			Container.BindFactory<IBoost, IBoost.Factory>().WithId(boostType).To<TBoost>();
		}
	}
}