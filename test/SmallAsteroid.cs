using Godot;
using System;

public partial class SmallAsteroid : Area2D
{
	[Export] public float Speed = 50.0f;
	
	private Vector2 direction;
	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready()
	{
		rng.Randomize();

		float angle = rng.RandfRange(0, Mathf.Tau);

		direction = Vector2.FromAngle(angle);
	}
	
	private void _on_body_entered(Node2D body)
	{
		if (body is Player player){
			GD.Print("Player entered");
			player.TakeDamage(20);
			QueueFree();
			}
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Position += direction * Speed * (float)delta;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnScreenExited()
	{
   		 QueueFree();
		GD.Print("Exited");
	}
}
