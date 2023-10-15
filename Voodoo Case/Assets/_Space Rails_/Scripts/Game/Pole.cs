using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public class Pole : MonoBehaviour
	{
		[System.Serializable]
		public class Settings
		{
			[Min(0f)] public float SegmentLength = 0.5f;
		}

		[SerializeField] private int _maxLength = 10;
		private Settings _settings;

		public float Length
		{
			get => GetLength();
			set => SetLength(Mathf.Min(value, _maxLength));
		}

		[Inject]
		private void Construct(Settings settings)
		{
			_settings = settings;
		}

		private float GetLength() => transform.localScale.y / _settings.SegmentLength;

		private void SetLength(float length)
		{
			var scale = transform.localScale;
			scale.y = length * _settings.SegmentLength;
			transform.localScale = scale;
		}
	}
}