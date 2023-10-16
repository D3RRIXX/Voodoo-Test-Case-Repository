using DG.Tweening;
using SpaceRails.Utilities;
using UnityEngine;

namespace SpaceRails.Game
{
	public class PlayerPoleHandler : MonoBehaviour
	{
		[SerializeField] private Pole _pole;
		[SerializeField] private float _restorePosDelay = 1f;
		[SerializeField] private float _tweenDuration = 0.5f;

		private bool _lostPole;
		private Sequence _sequence;

		public Pole Pole => _pole;

		public void LosePole()
		{
			if (_lostPole)
				return;
			
			Debug.Log("Lost pole!");
			_lostPole = true;
			Pole.transform.SetParent(null);
			Pole.gameObject.AddComponent<Rigidbody>();
		}
		
		public void CutOff(float length, Vector3 deltaPos)
		{
			_sequence.Kill();
			
			float newX = length * -Mathf.Sign(deltaPos.x);
			Pole.Length -= length;
			Pole.transform.localPosition = Pole.transform.localPosition.WithNewX(newX);

			_sequence = DOTween.Sequence()
			                   .AppendInterval(_restorePosDelay)
			                   .Append(Pole.transform.DOLocalMoveX(0f, _tweenDuration));
		}
	}
}