using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float MaxSpeed = 300.0f;
	public const float Acceleration = 200.0f;
	public const float Deceleration = 100.0f;
	private int hp = 100;
	
	public void TakeDamage(int damage)
	{
		hp -= damage;

		GD.Print("Player health: " + hp);

		if (hp <= 0)
		{
			Die();
		}
	}
	
	public void Heal(int healing){
		if (hp < 100){
		hp += healing;
		}
		
		GD.Print("Players health: " + hp);
	}

	private void Die()
	{
		GD.Print("Player died!");
		QueueFree();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//movement during key input
		if (direction != Vector2.Zero)
		{
			velocity = velocity.MoveToward(
				direction * MaxSpeed,
				Acceleration * (float)delta
			);
			float targetAngle = direction.Angle() + Mathf.Pi / 2;
			Rotation = Mathf.LerpAngle(
				Rotation ,
				targetAngle,
				5.0f * (float)delta
			);
		}
		//movement after the release of key input
		else
		{
			velocity = velocity.MoveToward(
				Vector2.Zero,
				Deceleration * (float)delta
				);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
