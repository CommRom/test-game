using Godot;
using System;

public partial class Game : Node2D
{
	[Export] public Hud HudNode;
	[Export] public Player PlayerNode;
	
	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		var deathScreen = GetNode<DeathScreen>("DeathScreen");
		player.Died += deathScreen.ShowDeath;
		GetNode<Button>("VBoxContainer/Button").Pressed += OnQuitPressed;
		PlayerNode.HealthChanged += HudNode.SetHp;
		HudNode.SetHp(PlayerNode.CurrentHp, PlayerNode.MaxHp);
	}
	
	private void OnQuitPressed()
	{
		GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
