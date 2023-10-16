using System.Linq;
using SpaceRails.Utilities;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public class RailPair : MonoBehaviour
	{
		[SerializeField] private float _gapWidth = 2f;
		[SerializeField] private Rail[] _rails;
		
		private PlayerPoleHandler _playerPoleHandler;

		private void Reset()
		{
			_rails = GetComponentsInChildren<Rail>();
		}

		[Inject]
		private void Construct(PlayerPoleHandler playerPoleHandler)
		{
			_playerPoleHandler = playerPoleHandler;
		}

		private void OnValidate()
		{
			float offset = _gapWidth / 2f;
			var localPosition = _rails[0].transform.localPosition;
			
			_rails[0].transform.localPosition = localPosition.WithNewX(-offset);
			_rails[1].transform.localPosition = localPosition.WithNewX(offset);
		}

		private void Update()
		{
			int railsTouchingPlayer = _rails.Count(x => x.TouchingPlayer.Value);

			if (railsTouchingPlayer == 1)
				_playerPoleHandler.LosePole();
		}
	}
}