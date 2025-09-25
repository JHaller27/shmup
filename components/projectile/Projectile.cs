using System.Collections.Generic;
using Godot;

public partial class Projectile : Area2D
{
	[Export]
	private double Speed = 100;

	[Export]
	private double Lifetime = 3;

	public HashSet<Hitbox> ExcludedColliders = new();

	public override void _Ready()
	{
		this.AreaEntered += this._OnAreaEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		this.GlobalPosition += Vector2.Up.Rotated(this.Rotation) * (float)(this.Speed * delta);
		this.Lifetime -= delta;
		if (this.Lifetime <= 0)
		{
			this.QueueFree();
		}
	}

	private void _OnAreaEntered(Area2D area2D)
	{
		if (area2D is not Hitbox hitbox) return;
		if (this.ExcludedColliders.Contains(hitbox)) return;

		hitbox.Hit(this);
		this.QueueFree();
	}
}
