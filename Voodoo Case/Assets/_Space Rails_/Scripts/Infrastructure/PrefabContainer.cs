using UnityEngine;

namespace SpaceRails.Infrastructure
{
	[CreateAssetMenu(fileName = "Prefab Container", menuName = "Space Rails/Prefab Container", order = 0)]
	public class PrefabContainer : ScriptableObject
	{
		[SerializeField] private GameObject _polePrefab;

		public GameObject PolePrefab => _polePrefab;
	}
}