using Godot;

public partial class Hitbox : Area2D
{
	[Signal]
	public delegate void OnHitEventHandler(Projectile projectile);

	public void Hit(Projectile projectile)
	{
		this.EmitSignal(SignalName.OnHit, projectile);
	}
}
