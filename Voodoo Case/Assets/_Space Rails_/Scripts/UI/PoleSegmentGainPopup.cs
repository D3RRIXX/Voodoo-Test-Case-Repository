using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpaceRails.UI
{
	public class PoleSegmentGainPopup : MonoBehaviour, IPoolable<string, IMemoryPool>, IDisposable
	{
		private const float TWEEN_DURATION = 1f;
		[SerializeField] private TMP_Text _label;
		
		private IMemoryPool _pool;
		private Sequence _sequence;
		private Color _cachedColor;

		private void Awake()
		{
			_cachedColor = _label.color;
		}

		private void OnDestroy()
		{
			_sequence.Kill();
		}

		public void OnDespawned()
		{
			_pool = null;
		}

		public void OnSpawned(string text, IMemoryPool pool)
		{
			_pool = pool;
			_label.color = _cachedColor;
			_label.text = text;

			_sequence = DOTween.Sequence()
			                   .Append(transform.DOPunchScale(Vector3.one * .15f, 0.5f, 2, 0.5f))
			                   .Insert(0f, transform.DOMoveY(TWEEN_DURATION, TWEEN_DURATION).SetRelative())
			                   .Insert(0f, _label.DOFade(0f, TWEEN_DURATION))
			                   .OnComplete(Dispose);
		}

		public void Dispose()
		{
			_pool.Despawn(this);
		}

		public class Factory : PlaceholderFactory<string, PoleSegmentGainPopup>
		{
		}
	}
}