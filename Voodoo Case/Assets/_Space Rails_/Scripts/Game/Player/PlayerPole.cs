using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Player
{
	public class PlayerPole : Pole
	{
		private SignalBus _signalBus;

		[Inject]
		private void Construct(SignalBus signalBus)
		{
			_signalBus = signalBus;
		}

		public override float Length
		{
			get => base.Length;
			set
			{
				base.Length = value;
				OnLengthChanged();
			}
		}

		private void OnLengthChanged()
		{
			_signalBus.Fire(new PlayerPoleLengthChangedSignal(this));
		}
	}
}