using Godot;

public partial class FuelBar : ProgressBar
{
	[Export]
	private Player Player;

	public override void _Process(double delta)
	{
		if (this.Player != null)
		{
			this.Value = this.Player.BoostFuel;
			this.MaxValue = this.Player.BoostFuelCapacity;
		}
	}
}
