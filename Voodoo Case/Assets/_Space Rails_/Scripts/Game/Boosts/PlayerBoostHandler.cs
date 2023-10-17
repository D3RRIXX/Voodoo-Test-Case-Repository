using System.Collections.Generic;
using UniRx;
using Zenject;

namespace SpaceRails.Game.Boosts
{
	internal class PlayerBoostHandler : ITickable
	{
		private readonly DiContainer _container;
		private readonly ReactiveDictionary<BoostType, BoostRunner> _activeBoosts = new();
		private readonly List<BoostType> _expiredBoosts = new();

		public PlayerBoostHandler(DiContainer container)
		{
			_container = container;
		}
		
		public IReadOnlyReactiveDictionary<BoostType, BoostRunner> ActiveBoosts => _activeBoosts;

		public bool HasBoost(BoostType boostType, out BoostRunner boostRunner) => _activeBoosts.TryGetValue(boostType, out boostRunner);
		public bool HasBoost(BoostType boostType) => _activeBoosts.TryGetValue(boostType, out _);
		
		public void AddBoost(BoostType boostType, float duration)
		{
			if (HasBoost(boostType, out BoostRunner boostRunner))
			{
				boostRunner.RemainingTime.Value = duration;
				return;
			}
			
			IBoost boost = _container.ResolveId<IBoost.Factory>(boostType).Create();
			_activeBoosts.Add(boostType, new BoostRunner(boostType, boost, duration));
		}

		public void Tick()
		{
			foreach ((BoostType boostType, BoostRunner boostRunner) in _activeBoosts)
			{
				boostRunner.Tick();
				
				if (boostRunner.RemainingTime.Value <= 0f)
					_expiredBoosts.Add(boostType);
			}

			DisposeExpiredBoosts();
		}

		private void DisposeExpiredBoosts()
		{
			if (_expiredBoosts.Count == 0)
				return;

			foreach (BoostType boostType in _expiredBoosts)
			{
				_activeBoosts[boostType].Dispose();
				_activeBoosts.Remove(boostType);
			}
			
			_expiredBoosts.Clear();
		}
	}
}