using System;
using Godot;

public partial class Player : Sprite2D
{
	[Export]
	private double Speed = 10;

	[Export]
	private float TurnRadius = 10;

	[Export]
	private double FireRate = 2;

	[Export]
	private PackedScene Ammo;

	private double TimeSinceLastFire;

	public override void _PhysicsProcess(double delta)
	{
		this.Rotation += Mathf.DegToRad(Input.GetAxis("player_left", "player_right"));
		this.GlobalPosition += Vector2.Up.Rotated(this.Rotation) * (float)(this.Speed * delta);

		this.TimeSinceLastFire = Math.Min(this.TimeSinceLastFire + delta, 1 / this.FireRate);

		while (Input.IsActionPressed("player_fire") && this.TimeSinceLastFire >= (1 / this.FireRate))
		{
			Bullet bullet = this.Ammo.Instantiate<Bullet>();
			bullet.ExcludedColliders.Add(this.GetNode<Hitbox>("%ActorCollision"));
			this.GetParent().AddChild(bullet);
			bullet.GlobalPosition = this.GlobalPosition;
			bullet.Rotation = this.Rotation;

			this.TimeSinceLastFire -= 1 / this.FireRate;
		}
	}

	private void _OnHit(Projectile weaponCollider)
	{
		this.Modulate = Colors.Red;
	}
}
