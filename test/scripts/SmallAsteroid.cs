using Godot;

public partial class SmallAsteroid : Area2D
{
	[Export] public float Speed = 60.0f;
	[Export] public int Damage = 20;
	[Export(PropertyHint.Range, "0,180,1")] public float SpreadDegrees = 35.0f;
	[Export] public float MinSpinDegrees = 20.0f;
	[Export] public float MaxSpinDegrees = 90.0f;

	private Vector2 direction;
	private float spin;
	private readonly RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready()
	{
		rng.Randomize();
		direction = PickDirection();

		float degrees = rng.RandfRange(MinSpinDegrees, MaxSpinDegrees);
		if (rng.Randf() < 0.5f) degrees = -degrees;
		spin = Mathf.DegToRad(degrees);
	}

	private Vector2 PickDirection()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null || !IsInstanceValid(player))
			return Vector2.FromAngle(rng.RandfRange(0, Mathf.Tau));

		float spread = Mathf.DegToRad(SpreadDegrees);
		return (player.GlobalPosition - GlobalPosition).Normalized()
			.Rotated(rng.RandfRange(-spread, spread));
	}

	private void _on_body_entered(Node2D body)
	{
		if (body is Player player)
		{
			player.TakeDamage(Damage);
			QueueFree();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += direction * Speed * (float)delta;
		Rotation += spin * (float)delta;
	}

	private void OnScreenExited()
	{
		QueueFree();
	}
}
