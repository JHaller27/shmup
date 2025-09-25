using Godot;

public partial class Projectile : Area2D
{
	[Signal]
	public delegate void OnHitEventHandler(Hitbox actorCollision);

	private void _OnAreaEntered(Area2D area2D)
	{
		this.EmitSignal(SignalName.OnHit, area2D as Hitbox);
		(area2D as Hitbox).Hit(this);
	}
}
