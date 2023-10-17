using SpaceRails.Game.Boosts;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceRails.UI
{
	public class BoostDisplayCell : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _nameLabel;
		[SerializeField] private Slider _slider;
		
		[Inject]
		private void Construct(BoostRunner boostRunner)
		{
			_nameLabel.text = boostRunner.BoostType.ToString();

			boostRunner.RemainingTime
			           .Select(x => x / boostRunner.Duration)
			           .Subscribe(x => _slider.value = x)
			           .AddTo(this);
		}
		
		public class Factory : PlaceholderFactory<BoostRunner, BoostDisplayCell>
		{
		}
	}
}