using SpaceRails.Infrastructure;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceRails.UI
{
	public class CurrencyDisplay : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _currencyLabel;
		
		[Inject]
		private void Construct(CurrencyManager currencyManager)
		{
			currencyManager.Coins.Subscribe(UpdateText);
		}

		private void UpdateText(int currencyAmount)
		{
			_currencyLabel.text = $"Coins: {currencyAmount}";
		}
	}
}