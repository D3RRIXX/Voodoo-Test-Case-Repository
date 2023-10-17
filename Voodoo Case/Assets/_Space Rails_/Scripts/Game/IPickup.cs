using SpaceRails.Infrastructure;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game
{
	public interface IPickup
	{
		void OnPickup(GameObject instigator);
	}

	[RequireComponent(typeof(Collider))]
	public abstract class PickupBase : MonoBehaviour, IPickup
	{
		[SerializeField] private AudioClip _pickupClip;
		[Inject] private AudioPlayer _audioPlayer;
		
		public virtual void OnPickup(GameObject instigator)
		{
			_audioPlayer.Play(_pickupClip);
			Destroy(gameObject);
		}
	}
}