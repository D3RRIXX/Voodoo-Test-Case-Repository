namespace SpaceRails.Game.Player
{
	public class PlayerPoleLengthChangedSignal
	{
		public Pole Pole { get; }

		public PlayerPoleLengthChangedSignal(Pole pole)
		{
			Pole = pole;
		}
	}
}