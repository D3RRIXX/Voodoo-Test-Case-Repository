using UnityEngine;

namespace SpaceRails.Game.Obstacles
{
	public class Mine : Obstacle
	{
		[SerializeField] private ParticleSystem _vfxPrefab;
		
		protected override void OnTouchedPlayer(Collision other)
		{
			PlayVFX();
		
			var poleHandler = other.gameObject.GetComponent<PlayerPoleHandler>();
			poleHandler.OnTouchedMine();
		}

		private void PlayVFX()
		{
			Instantiate(_vfxPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}