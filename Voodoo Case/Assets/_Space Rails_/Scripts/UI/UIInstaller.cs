using SpaceRails.Game.Boosts;
using UnityEngine;
using Zenject;

namespace SpaceRails.UI
{
	public class UIInstaller : MonoInstaller
	{
		[SerializeField] private BoostDisplayCell _boostDisplayCellPrefab;
		
		public override void InstallBindings()
		{
			Container.BindFactory<BoostRunner, BoostDisplayCell, BoostDisplayCell.Factory>()
			         .FromComponentInNewPrefab(_boostDisplayCellPrefab);
		}
	}
}