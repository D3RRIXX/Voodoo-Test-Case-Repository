using System;
using SpaceRails.Utilities;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public class Pole : MonoBehaviour
	{
		[Serializable]
		public class Settings
		{
			[Min(0f)] public float SegmentLength = 0.5f;
		}

		[SerializeField] private int _maxLength = 10;
		private Settings _settings;

		public float Length
		{
			get => transform.localScale.y;
			set => transform.localScale = transform.localScale.WithNewY(value);
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


		private void OnCollisionEnter(Collision other)
		{
			Debug.Log($"Touched {other.gameObject.name}", other.gameObject);
		}
	}
}