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
	private double FireRate = 2;

	[Export]
	private PackedScene Ammo;

	private double TimeSinceLastFire;

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

		this.TimeSinceLastFire = Math.Min(this.TimeSinceLastFire + delta, 1 / this.FireRate);

		while (this.RayCast2D.IsColliding() && this.TimeSinceLastFire >= (1 / this.FireRate))
		{
			Bullet bullet = this.Ammo.Instantiate<Bullet>();
			bullet.ExcludedColliders.Add(this.GetNode<ActorCollision>("%ActorCollision"));
			this.GetParent().AddChild(bullet);
			bullet.GlobalPosition = this.GlobalPosition;
			bullet.Rotation = this.Rotation;

			this.TimeSinceLastFire -= 1 / this.FireRate;
		}
	}

	public override void _Process(double delta)
	{
		this.CurrentTarget = this.GetTree().GetNodesInGroup(Constants.Groups.ALLIES).MinBy(n => this.GlobalPosition - (n as Node2D).GlobalPosition) as Node2D ?? this.CurrentTarget;
	}

	private void _OnHit(WeaponCollider weaponCollider)
	{
		this.Modulate = Colors.Purple;
	}
}
