using Godot;

public partial class Health : ProgressBar
{
	[Signal]
	public delegate void HealthDepletedEventHandler();

	[Export]
	private Hitbox Hitbox;

	public override void _Ready()
	{
		this.Hitbox.OnHit += this._OnHit;
		this.Value = this.MaxValue;
	}

	private void _OnHit(Projectile projectile)
	{
		this.Value -= 1;
		if (this.Value <= 0)
		{
			this.EmitSignal(SignalName.HealthDepleted);
		}
	}
}
