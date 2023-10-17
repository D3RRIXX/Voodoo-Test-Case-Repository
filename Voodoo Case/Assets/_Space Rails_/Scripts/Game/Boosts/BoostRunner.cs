using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceRails.Game.Boosts
{
	public class BoostRunner : ITickable, IDisposable
	{
		private readonly IBoost _boost;

		public BoostRunner(BoostType boostType, IBoost boost, float duration)
		{
			_boost = boost;
			RemainingTime = new ReactiveProperty<float>(duration);
			_boost.Apply();
			
			BoostType = boostType;
		}

		public BoostType BoostType { get; }
		public ReactiveProperty<float> RemainingTime { get; }

		public void Tick()
		{
			RemainingTime.Value -= Time.deltaTime;
		}

		public void Dispose()
		{
			_boost.Dispose();
		}
	}
}