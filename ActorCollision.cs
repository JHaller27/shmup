using Godot;

public partial class ActorCollision : Area2D
{
	[Signal]
	public delegate void WeaponCollisionEventHandler(WeaponCollider weaponCollider);

	public void Hit(WeaponCollider weaponCollider)
	{
		this.EmitSignal(SignalName.WeaponCollision, weaponCollider);
	}
}
