using System;
using Godot;

public partial class ProjectileEmitter : Node2D
{
	[Export]
	private PackedScene ProjectileScene;

	[Export]
	private Hitbox[] ExcludedHitboxes;

	[Export]
	private double FireRate = 2;

	private double TimeSinceLastFire;

	public override void _PhysicsProcess(double delta)
	{
		this.TimeSinceLastFire = Math.Min(this.TimeSinceLastFire + delta, 1 / this.FireRate);
	}

	public bool CanFire() => this.TimeSinceLastFire >= (1 / this.FireRate);

	public Projectile EmitProjectile()
	{
		if (!this.CanFire()) return null;

		Projectile projectile = this.ProjectileScene.Instantiate<Projectile>();
		this.GetTree().CurrentScene.AddChild(projectile);

		foreach (Hitbox excluded in this.ExcludedHitboxes)
		{
			projectile.ExcludedColliders.Add(excluded);
		}

		projectile.GlobalPosition = this.GlobalPosition;
		projectile.GlobalRotation = this.GlobalRotation;

		this.TimeSinceLastFire -= 1 / this.FireRate;

		return projectile;
	}
}
