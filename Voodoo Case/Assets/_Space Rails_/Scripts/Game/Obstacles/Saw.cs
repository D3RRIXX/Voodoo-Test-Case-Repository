using SpaceRails.Game.Boosts;
using SpaceRails.Game.Player;
using SpaceRails.Utilities;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Obstacles
{
	public class Saw : Obstacle
	{
		[SerializeField] private ParticleSystem _explosionVfxPrefab;
		
		private PlayerBoostHandler _playerBoostHandler;

		[Inject]
		private void Construct(PlayerBoostHandler playerBoostHandler)
		{
			_playerBoostHandler = playerBoostHandler;
		}
		
		protected override void OnTouchedPlayer(Collision other)
		{
			if (_playerBoostHandler.HasBoost(BoostType.IronPole))
			{
				Instantiate(_explosionVfxPrefab, transform.position, Quaternion.identity);
				Destroy(gameObject);
				return;
			}
			
			var poleHandler = other.gameObject.GetComponent<PlayerPoleHandler>();
			var pole = poleHandler.Pole;
			Vector3 polePosition = pole.transform.position;
			
			ContactPoint contact = GetClosestContact(other, polePosition);
			Vector3 delta = contact.point - polePosition;
			float deltaLength = (pole.Length - Mathf.Abs(delta.x)) / 2;

			poleHandler.CutOff(deltaLength, delta);

			Collider.enabled = false;
		}

		private static ContactPoint GetClosestContact(Collision other, Vector3 polePosition)
		{
			int contactCount = other.contactCount;
			float minDelta = GetDistance(0);
			int minDeltaIndex = 0;
				
			for (int i = 1; i < contactCount; i++)
			{
				float delta = GetDistance(i);
				if (delta >= minDelta)
					continue;
					
				minDelta = delta;
				minDeltaIndex = i;
			}

			return other.GetContact(minDeltaIndex);

			float GetDistance(int index) => Vector3.Distance(other.GetContact(index).point, polePosition);
		}
	}
}