using System;
using Godot;

public partial class Player : Sprite2D
{
	[Export]
	private double Speed = 10;

	[Export]
	private float TurnRadius = 10;

	[Export]
	private ProjectileEmitter ProjectileEmitter;

	private bool IsAlive = true;

	public override void _PhysicsProcess(double delta)
	{
		if (!this.IsAlive) return;

		this.Rotation += Mathf.DegToRad(Input.GetAxis("player_left", "player_right"));
		this.GlobalPosition += Vector2.Up.Rotated(this.Rotation) * (float)(this.Speed * delta);

		while (Input.IsActionPressed("player_fire") && this.ProjectileEmitter.CanFire())
		{
			this.ProjectileEmitter.EmitProjectile();
		}
	}

	private void _on_health_health_depleted()
	{
		this.Modulate = Colors.Red;
		this.IsAlive = false;
	}
}
