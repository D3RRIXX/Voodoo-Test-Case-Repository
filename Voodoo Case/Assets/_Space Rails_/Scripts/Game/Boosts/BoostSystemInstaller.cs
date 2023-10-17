using SpaceRails.Infrastructure;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Boosts
{
	[CreateAssetMenu(fileName = "Boost System Installer", menuName = SOPaths.INSTALLERS + "Boost System")]
	public class BoostSystemInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private MagnetBoost.Settings _magnetSettings;
		[SerializeField] private IronPoleBoost.Settings _ironPoleSettings;
		[SerializeField] private ShieldBoost.Settings _shieldSettings;
		
		public override void InstallBindings()
		{
			Container.BindInstance(_magnetSettings);
			Container.BindInstance(_ironPoleSettings);
			Container.BindInstance(_shieldSettings);

			BindBoostFactory<MagnetBoost>(BoostType.Magnet);
			BindBoostFactory<LavaBootsBoost>(BoostType.LavaBoots);
			BindBoostFactory<IronPoleBoost>(BoostType.IronPole);
			BindBoostFactory<ShieldBoost>(BoostType.Shield);
			
			Container.BindInterfacesAndSelfTo<PlayerBoostHandler>().AsSingle();
		}

		private void BindBoostFactory<TBoost>(BoostType boostType) where TBoost : IBoost
		{
			Container.BindFactory<IBoost, IBoost.Factory>().WithId(boostType).To<TBoost>();
		}
	}
}