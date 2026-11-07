using Godot;
using System;

public partial class BatterySpawner : Node2D
{
	[Export] public PackedScene BatteryScene;

	[Export] public float SpawnInterval = 5.0f;

	private Timer spawnTimer;
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	
	public override void _Ready()
	{
		rng.Randomize();

		spawnTimer = new Timer();
		AddChild(spawnTimer);

		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Timeout += SpawnBattery;
		spawnTimer.Start();
	}
	
	private void SpawnBattery()
	{
		Battery battery = BatteryScene.Instantiate<Battery>();

		GetTree().CurrentScene.AddChild(battery);

		// Get the visible screen area.
		Transform2D toWorld = GetViewport().GetCanvasTransform().AffineInverse();
		Rect2 view = GetViewport().GetVisibleRect();
		Vector2 topLeft = toWorld * view.Position;
		Vector2 bottomRight = toWorld * view.End;

		// Choose a random position on the screen.
		float x = rng.RandfRange(topLeft.X, bottomRight.X);
		float y = rng.RandfRange(topLeft.Y, bottomRight.Y);

		battery.GlobalPosition = new Vector2(x, y);
	}
}
