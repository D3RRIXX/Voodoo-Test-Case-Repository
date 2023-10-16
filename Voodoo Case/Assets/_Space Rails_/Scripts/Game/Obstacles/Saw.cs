using SpaceRails.Utilities;
using UnityEngine;

namespace SpaceRails.Game.Obstacles
{
	public class Saw : Obstacle
	{
		protected override void OnTouchedPlayer(Collision other)
		{
			var pole = other.gameObject.GetComponentInChildren<Pole>();
			Vector3 polePosition = pole.transform.position;
			
			ContactPoint contact = GetClosestContact(other, polePosition);
			Vector3 delta = contact.point - polePosition;
			float deltaLength = (pole.Length - Mathf.Abs(delta.x)) / 2;

			pole.CutOff(deltaLength, delta);

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