using UniRx;

namespace SpaceRails.Infrastructure
{
	public class CurrencyManager
	{
		public ReactiveProperty<int> Coins { get; } = new();
	}
}