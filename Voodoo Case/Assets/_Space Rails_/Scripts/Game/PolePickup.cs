using SpaceRails.Game.Player;
using SpaceRails.UI;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public class PolePickup : PickupBase
	{
		[SerializeField, Min(1)] private int _poleGain;
		[SerializeField] private Pole _pole;
		
		private PoleSegmentGainPopup.Factory _popupFactory;

		private void Reset()
		{
			_pole = GetComponentInChildren<Pole>();
		}

		[Inject]
		private void Construct(PoleSegmentGainPopup.Factory popupFactory)
		{
			_popupFactory = popupFactory;
		}

		private void Start()
		{
			_pole.Segments = _poleGain;
		}

		public override void OnPickup(GameObject instigator)
		{
			instigator.GetComponent<PlayerPoleHandler>().Pole.AddSegments(_poleGain);
			PoleSegmentGainPopup popup = _popupFactory.Create($"+{_poleGain.ToString()}");
			popup.transform.position = transform.position + Vector3.up * 2f;

			base.OnPickup(instigator);
		}
	}
}