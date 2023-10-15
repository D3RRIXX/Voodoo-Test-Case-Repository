using Sirenix.OdinInspector;
using UnityEngine;

namespace SpaceRails.Game
{
	public class PoleController : MonoBehaviour
	{
		[SerializeField] private Transform _poleTransform;
		[SerializeField] private float _poleSegmentLength = 0.5f;
		[SerializeField] private int _maxLength = 10;

		public float PoleLength
		{
			get => GetLength();
			set => SetLength(Mathf.Min(value, _maxLength));
		}
		
		[Button]
		private void AddLength()
		{
			PoleLength++;
		}

		[Button]
		private void RemoveLength()
		{
			PoleLength--;
		}

		private float GetLength() => _poleTransform.localScale.y / _poleSegmentLength;
		
		private void SetLength(float length)
		{
			var scale = _poleTransform.localScale;
			scale.y = length * _poleSegmentLength;
			_poleTransform.localScale = scale;
		}
	}
}