using System;
using Zenject;

namespace SpaceRails.Game.Boosts
{
	public interface IBoost : IDisposable
	{
		void Apply();
		
		public class Factory : PlaceholderFactory<IBoost>
		{
		}
	}
}