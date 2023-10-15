using UnityEngine;

namespace SpaceRails.Game
{
	public class PlayerPoleHandler : MonoBehaviour
	{
		[SerializeField] private Pole _pole;
		
		public Pole Pole => _pole;
	}
}