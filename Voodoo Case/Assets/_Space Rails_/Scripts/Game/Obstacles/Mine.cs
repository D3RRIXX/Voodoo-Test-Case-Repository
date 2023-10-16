using UnityEngine;

namespace SpaceRails.Game.Obstacles
{
	public class Mine : Obstacle
	{
		protected override void OnTouchedPlayer(Collision other)
		{
			PlayVFX();
		
			var poleHandler = other.gameObject.GetComponent<PlayerPoleHandler>();
			poleHandler.OnTouchedMine();
		}

		private void PlayVFX()
		{
			Destroy(gameObject);
		}
	}
}