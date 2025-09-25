using System;
using System.Linq;
using Godot;

public partial class Enemy : Sprite2D
{
	[Export]
	private double Speed = 40;

	[Export]
	private float TurnRadius = 10;

	[Export]
	private ProjectileEmitter ProjectileEmitter;

	private Node2D CurrentTarget;

	private RayCast2D RayCast2D;

	public override void _Ready()
	{
		this.RayCast2D = this.GetNode<RayCast2D>("%RayCast2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (this.CurrentTarget is not null)
		{
			float angleToTarget = this.GlobalPosition.AngleToPoint(this.CurrentTarget.GlobalPosition) + Mathf.Pi / 2;
			float turnBy = angleToTarget - this.GlobalRotation;
			if (turnBy > Mathf.Pi)
			{
				turnBy -= 2 * Mathf.Pi;
			}
			if (turnBy < Mathf.Pi / 2 && turnBy > -Mathf.Pi / 2)
			{
				this.Rotation += Math.Clamp(turnBy, -Mathf.DegToRad(this.TurnRadius), Mathf.DegToRad(this.TurnRadius));
			}
		}

		this.GlobalPosition += Vector2.Up.Rotated(this.Rotation) * (float)(this.Speed * delta);

		while (this.RayCast2D.IsColliding() && this.ProjectileEmitter.CanFire())
		{
			this.ProjectileEmitter.EmitProjectile();
		}
	}

	public override void _Process(double delta)
	{
		this.CurrentTarget = this.GetTree().GetNodesInGroup(Constants.Groups.ALLIES)
			.Cast<Player>()
			.Where(p => p.IsAlive)
			.MinBy(p => this.GlobalPosition - p.GlobalPosition);
	}

	private void _on_health_health_depleted()
	{
		this.QueueFree();
	}
}
