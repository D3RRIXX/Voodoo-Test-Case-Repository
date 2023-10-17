using SpaceRails.Game.Obstacles;
using SpaceRails.Game.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SpaceRails.Game
{
	public class Rail : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _sparksVFX;
		
		public ReactiveProperty<bool> TouchingPlayer { get; } = new();

		private void Awake()
		{
			this.OnCollisionStayAsObservable()
			    .Where(x => x.gameObject.CompareTag("Player"))
			    .Select(x => x.GetContact(0).point)
			    .Subscribe(point => _sparksVFX.transform.position = point)
			    .AddTo(this);
		}

		private void OnCollisionEnter(Collision other)
		{
			if (!other.gameObject.CompareTag("Player"))
				return;
			
			_sparksVFX.Play(true);
			other.gameObject.GetComponent<PlayerMovement>().ChangeMovementType(MovementType.Rails);
			TouchingPlayer.Value = true;
		}

		private void OnCollisionExit(Collision other)
		{
			if (!other.gameObject.CompareTag("Player"))
				return;
			
			_sparksVFX.Stop(true);
			TouchingPlayer.Value = false;
		}
	}
}