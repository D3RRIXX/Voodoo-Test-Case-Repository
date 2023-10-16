using SpaceRails.Game.Obstacles;
using UniRx;
using UnityEngine;

namespace SpaceRails.Game
{
	public class Rail : MonoBehaviour
	{
		public ReactiveProperty<bool> TouchingPlayer { get; } = new();

		private void OnCollisionEnter(Collision other)
		{
			if (!other.gameObject.CompareTag("Player"))
				return;
			
			TouchingPlayer.Value = true;
		}
		
		private void OnCollisionExit(Collision other)
		{
			if (!other.gameObject.CompareTag("Player"))
				return;
			
			TouchingPlayer.Value = false;
		}
	}
}