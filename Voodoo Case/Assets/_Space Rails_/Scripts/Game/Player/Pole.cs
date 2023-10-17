using System;
using SpaceRails.Utilities;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Player
{
	public class Pole : MonoBehaviour
	{
		[Serializable]
		public class Settings
		{
			[Min(0f)] public float SegmentLength = 0.5f;
		}

		private Settings _settings;

		public virtual float Length
		{
			get => transform.localScale.y;
			set => transform.localScale = transform.localScale.WithNewY(Mathf.Max(value, 0f));
		}
		
		public int Segments
		{
			get => Mathf.RoundToInt(Length / _settings.SegmentLength);
			set => Length = Mathf.Max(0, value) * _settings.SegmentLength;
		}

		[Inject]
		private void Construct(Settings settings)
		{
			_settings = settings;
		}

		public void AddSegments(int count)
		{
			Length += count * _settings.SegmentLength;
		}
	}
}