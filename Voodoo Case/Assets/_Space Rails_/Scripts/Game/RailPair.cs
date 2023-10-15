using System.Linq;
using SpaceRails.Utilities;
using UnityEngine;

namespace SpaceRails.Game
{
	public class RailPair : MonoBehaviour
	{
		[SerializeField] private Transform[] _rails;
		[SerializeField] private float _gapWidth = 2f;

		private void Reset()
		{
			_rails = transform.Cast<Transform>().ToArray();
		}

		private void OnValidate()
		{
			float offset = _gapWidth / 2f;
			var localPosition = _rails[0].localPosition;
			
			_rails[0].localPosition = localPosition.WithNewX(-offset);
			_rails[1].localPosition = localPosition.WithNewX(offset);
		}
	}
}