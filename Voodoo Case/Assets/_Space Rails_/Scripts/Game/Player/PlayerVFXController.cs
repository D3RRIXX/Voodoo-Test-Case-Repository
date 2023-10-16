using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using SpaceRails.Game.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public class PlayerVFXController : MonoBehaviour
	{
		[SerializeField] private SkinnedMeshRenderer[] _meshRenderers;

		[SerializeField] private Material _originalMaterial;
		[SerializeField] private Material _transparentMaterial;
		
		private Tween _tween;

		[Inject]
		private void Construct(SignalBus signalBus)
		{
			signalBus.GetStream<PlayerInvincibilityStartedSignal>()
			         .Subscribe(OnInvincibilityStarted)
			         .AddTo(this);
		}

		private void OnDestroy()
		{
			_tween.Kill();
		}

		private void OnInvincibilityStarted(PlayerInvincibilityStartedSignal signal)
		{
			ApplyMaterial(_transparentMaterial);
			_tween = _transparentMaterial.DOFade(0f, signal.Duration).SetEase(Ease.Flash, 30, 0)
			                             .OnComplete(() => ApplyMaterial(_originalMaterial));
		}
		
		private void ApplyMaterial(Material material)
		{
			foreach (SkinnedMeshRenderer meshRenderer in _meshRenderers)
			{
				meshRenderer.material = material;
			}
		}
	}
}