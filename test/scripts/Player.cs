using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float MaxSpeed = 300.0f;
	public const float Acceleration = 200.0f;
	public const float Deceleration = 100.0f;
	private int hp = 80;
	public int MaxHp = 80;
	private bool isDead = false;
	public int CurrentHp => hp;
	
	[Signal] public delegate void DiedEventHandler();
	[Signal] public delegate void HealthChangedEventHandler(int current, int max);
	
	public void TakeDamage(int damage)
	{
		if (isDead) return;
		
		hp = Mathf.Max(hp - damage, 0);

		GD.Print("Player health: " + hp);
		
		EmitSignal(SignalName.HealthChanged, hp, MaxHp);

		if (hp <= 0)
		{
			isDead = true;
			Die();
		}
		
	}
	
	public void Heal(int healing){
		if (hp < MaxHp){
		hp += healing;
		}
		EmitSignal(SignalName.HealthChanged, hp, MaxHp);
		
		GD.Print("Players health: " + hp);
	}

	private void Die()
	{
		GD.Print("Player died!");
		QueueFree();
		EmitSignal(SignalName.Died);
	}
	
	public override void _Ready()
	{
		hp = MaxHp;
		EmitSignal(SignalName.HealthChanged, hp, MaxHp);
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
