using UnityEngine;

namespace SpaceRails.Game
{
	public class PlayerPoleHandler : MonoBehaviour
	{
		[SerializeField] private Pole _pole;

		private bool _lostPole;
		
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
	}
}