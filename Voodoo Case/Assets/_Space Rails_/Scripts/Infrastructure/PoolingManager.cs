using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace SpaceRails.Infrastructure
{
	using Original = Component;
	using Instance = Component;

	[UsedImplicitly]
	public class PoolingManager
	{
		private readonly Dictionary<Original, Pool> cached = new();
		private readonly Dictionary<Instance, Original> borrowed = new();

		private readonly Transform poolsParent;
		private readonly DiContainer _container;

		public PoolingManager(DiContainer container)
		{
			_container = container;
			poolsParent = new GameObject("Pools").transform;

			Object.DontDestroyOnLoad(poolsParent);
		}

		public T GetObject<T>(T original) where T : Component => GetObject(original, Vector3.zero, Quaternion.identity, null);

		public T GetObject<T>(T original, Transform parent) where T : Component => GetObject(original, Vector3.zero, Quaternion.identity, parent);

		public T GetObject<T>(T original, Vector3 position, Quaternion rotation) where T : Component => GetObject(original, position, rotation, null);

		public T GetObject<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Component
		{
			T clone = GetClone(original, parent);
			clone.transform.SetPositionAndRotation(position, rotation);

			borrowed.Add(clone, original);

			return clone;
		}

		public void ReturnObject<T>(T clone) where T : Component
		{
			if (!borrowed.TryGetValue(clone, out Component original))
				throw new ArgumentException($"Object {clone.name} isn't related to any pool! Can't return it", nameof(clone));

			Pool pool = cached[original];
			pool.Objects.Push(clone);

			clone.gameObject.SetActive(false);
			clone.transform.SetParent(pool.PoolParent);

			borrowed.Remove(clone);
		}

		private T GetClone<T>(T original, Transform parent) where T : Component
		{
			if (cached.TryGetValue(original, out Pool pool))
			{
				if (pool.Objects.Count > 0)
				{
					var clone = (T)pool.Objects.Pop();
					clone.transform.SetParent(parent);
					clone.gameObject.SetActive(true);

					return clone;
				}
			}
			else
			{
				pool = new Pool(poolsParent, original.name);
				cached[original] = pool;
			}

			return InstantiateClone(original, parent);
		}

		private void AddPool(Component original, int amount)
		{
			if (!cached.TryGetValue(original, out Pool pool))
			{
				pool = new Pool(poolsParent, original.name);

				cached.Add(original, pool);
			}

			original.gameObject.SetActive(false);

			for (int i = 0; i < amount; i++)
			{
				Component clone = InstantiateClone(original, pool.PoolParent);
				pool.Objects.Push(clone);
			}

			original.gameObject.SetActive(true);
		}

		private T InstantiateClone<T>(T original, Transform parent) where T : Component
		{
			return _container.InstantiatePrefabForComponent<T>(original, parent);
		}
	}
}