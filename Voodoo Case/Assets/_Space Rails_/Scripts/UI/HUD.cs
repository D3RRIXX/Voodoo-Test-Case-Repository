using SpaceRails.Game.Player;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceRails.UI
{
	public class HUD : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _poleLengthLabel;
		
		[Inject]
		private void Construct(SignalBus signalBus, PlayerPoleHandler poleHandler)
		{
			SetPoleLengthText(poleHandler.Pole.Segments);
			signalBus.GetStream<PlayerPoleLengthChangedSignal>()
			         .Subscribe(signal => SetPoleLengthText(signal.Pole.Segments))
			         .AddTo(this);
		}

		private void SetPoleLengthText(int poleSegments)
		{
			_poleLengthLabel.text = poleSegments.ToString();
		}
	}
}