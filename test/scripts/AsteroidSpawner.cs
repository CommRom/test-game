using Godot;
using System;

public partial class AsteroidSpawner : Node2D
{
	[Export] public PackedScene AsteroidScene;

	[Export] public float SpawnInterval = 0.8f;

	private Timer spawnTimer;
	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready()
	{
		rng.Randomize();

		spawnTimer = new Timer();
		AddChild(spawnTimer);

		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Timeout += SpawnAsteroid;
		spawnTimer.Start();
	}

	private void SpawnAsteroid()
	{
		SmallAsteroid asteroid = AsteroidScene.Instantiate<SmallAsteroid>();

		GetTree().CurrentScene.AddChild(asteroid);

		// Get the visible screen area.
		Transform2D toWorld = GetViewport().GetCanvasTransform().AffineInverse();
		Rect2 view = GetViewport().GetVisibleRect();
		Vector2 topLeft = toWorld * view.Position;
		Vector2 bottomRight = toWorld * view.End;

		// Choose a random position on the screen.
		float x = rng.RandfRange(topLeft.X, bottomRight.X);
		float y = rng.RandfRange(topLeft.Y, bottomRight.Y);

		asteroid.GlobalPosition = new Vector2(x, y);
	}
}
