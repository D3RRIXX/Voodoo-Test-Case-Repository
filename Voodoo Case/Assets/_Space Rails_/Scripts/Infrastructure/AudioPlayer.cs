using UnityEngine;

namespace SpaceRails.Infrastructure
{
	public class AudioPlayer
	{
		private readonly AudioSource _audioSource;

		public AudioPlayer(AudioSource audioSource)
		{
			_audioSource = audioSource;
		}

		public void Play(AudioClip clip) => _audioSource.PlayOneShot(clip);
	}
}