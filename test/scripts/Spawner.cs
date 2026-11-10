using Godot;

public partial class Spawner : Node2D
{
	[Export] public PackedScene ItemScene;
	[Export] public float SpawnInterval = 2.0f;
	[Export] public float StartDelay = 0.0f;       // wait before the first spawn
	[Export] public float EdgeMargin = 32.0f;      // keep items off the screen edges
	[Export] public Node2D AvoidTarget;            // e.g. the player
	[Export] public float AvoidRadius = 150.0f;    // don't spawn this close to it

	private Timer spawnTimer;
	private readonly RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready()
	{
		rng.Randomize();

		spawnTimer = new Timer();
		AddChild(spawnTimer);
		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Timeout += Spawn;

		if (StartDelay > 0.0f)
		GetTree().CreateTimer(StartDelay).Timeout += () => spawnTimer.Start();
	else
		spawnTimer.Start();
	}

	private void Spawn()
	{
		if (ItemScene == null)
		{
			GD.PrintErr($"{Name}: ItemScene is not assigned.");
			return;
		}

		Node2D item = ItemScene.Instantiate<Node2D>();
		item.GlobalPosition = GetSpawnPosition();
		GetParent().AddChild(item);
	}

	private Vector2 GetSpawnPosition()
	{
		// Visible area in world coordinates (accounts for Camera2D).
		Transform2D toWorld = GetViewport().GetCanvasTransform().AffineInverse();
		Rect2 view = GetViewport().GetVisibleRect();
		Vector2 topLeft = toWorld * view.Position + new Vector2(EdgeMargin, EdgeMargin);
		Vector2 bottomRight = toWorld * view.End - new Vector2(EdgeMargin, EdgeMargin);

		Vector2 pos = Vector2.Zero;

		// Try a few times to find a spot away from the target, then give up and use the last one.
		for (int i = 0; i < 10; i++)
		{
			pos = new Vector2(
				rng.RandfRange(topLeft.X, bottomRight.X),
				rng.RandfRange(topLeft.Y, bottomRight.Y)
			);

			if (AvoidTarget == null || !IsInstanceValid(AvoidTarget)
				|| pos.DistanceTo(AvoidTarget.GlobalPosition) > AvoidRadius)
				break;
		}

		return pos;
	}
}
