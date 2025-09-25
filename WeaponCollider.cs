using Godot;

public partial class WeaponCollider : Area2D
{
	[Signal]
	public delegate void OnHitEventHandler(ActorCollision actorCollision);

	private void _OnAreaEntered(Area2D area2D)
	{
		this.EmitSignal(SignalName.OnHit, area2D as ActorCollision);
		(area2D as ActorCollision).Hit(this);
	}
}
