using System.Collections.Generic;
using Godot;

public partial class Bullet : Sprite2D
{
	[Export]
	private double Speed = 100;

	[Export]
	private double Lifetime = 3;

	public HashSet<ActorCollision> ExcludedColliders = new();

	public override void _PhysicsProcess(double delta)
	{
		this.GlobalPosition += Vector2.Up.Rotated(this.Rotation) * (float)(this.Speed * delta);
		this.Lifetime -= delta;
		if (this.Lifetime <= 0)
		{
			this.QueueFree();
		}
	}

	private void _OnHit(ActorCollision actorCollision)
	{
		if (this.ExcludedColliders.Contains(actorCollision)) return;
		this.QueueFree();
	}
}
