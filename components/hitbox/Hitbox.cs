using Godot;

public partial class Hitbox : Area2D
{
	[Signal]
	public delegate void WeaponCollisionEventHandler(Projectile weaponCollider);

	public void Hit(Projectile weaponCollider)
	{
		this.EmitSignal(SignalName.WeaponCollision, weaponCollider);
	}
}
