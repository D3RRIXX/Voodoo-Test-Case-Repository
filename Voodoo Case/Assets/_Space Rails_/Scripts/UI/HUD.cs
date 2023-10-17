using System.Collections.Generic;
using SpaceRails.Game.Boosts;
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
		[SerializeField] private Transform _boostCellContainer;
		
		private BoostDisplayCell.Factory _boostCellFactory;
		private Dictionary<BoostType, BoostDisplayCell> _displayCells = new();

		[Inject]
		private void Construct(SignalBus signalBus, PlayerPoleHandler poleHandler, PlayerBoostHandler playerBoostHandler, BoostDisplayCell.Factory boostCellFactory)
		{
			_boostCellFactory = boostCellFactory;
			SetPoleLengthText(poleHandler.Pole.Segments);

			var disposables = new CompositeDisposable();

			playerBoostHandler.ActiveBoosts
			                  .ObserveAdd()
			                  .Select(x => x.Value)
			                  .Subscribe(OnBoostAdded)
			                  .AddTo(disposables);
			
			playerBoostHandler.ActiveBoosts
			                  .ObserveRemove()
			                  .Select(x => x.Value)
			                  .Subscribe(OnBoostRemoved)
			                  .AddTo(disposables);

			signalBus.GetStream<PlayerPoleLengthChangedSignal>()
			         .Subscribe(signal => SetPoleLengthText(signal.Pole.Segments))
			         .AddTo(disposables);

			disposables.AddTo(this);
		}

		private void SetPoleLengthText(int poleSegments)
		{
			_poleLengthLabel.text = poleSegments.ToString();
		}

		private void OnBoostAdded(BoostRunner boostRunner)
		{
			Debug.Log($"Added boost: {boostRunner.BoostType}");
			BoostDisplayCell boostDisplayCell = _boostCellFactory.Create(boostRunner);
			boostDisplayCell.transform.SetParent(_boostCellContainer, false);
			
			_displayCells.Add(boostRunner.BoostType, boostDisplayCell);
		}

		private void OnBoostRemoved(BoostRunner boostRunner)
		{
			Debug.Log($"Removed boost: {boostRunner.BoostType}");
			
			Destroy(_displayCells[boostRunner.BoostType].gameObject);
			_displayCells.Remove(boostRunner.BoostType);
		}
	}
}