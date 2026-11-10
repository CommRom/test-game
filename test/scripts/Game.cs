using Godot;
using System;

public partial class Game : Node2D
{
	[Export] public Hud HudNode;
	[Export] public Player PlayerNode;
	[Export] public DeathScreen DeathScreenNode;
	
	private double elapsed;
	private bool running = true;
	
	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		PlayerNode.ScoreChanged += HudNode.SetScore;
		var deathScreen = GetNode<DeathScreen>("DeathScreen");
		GetNode<Button>("VBoxContainer/Button").Pressed += OnQuitPressed;
		PlayerNode.HealthChanged += HudNode.SetHp;
		PlayerNode.Died += OnPlayerDied;
		HudNode.SetHp(PlayerNode.CurrentHp, PlayerNode.MaxHp);
		HudNode.SetScore(0);
	}
	
	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
	
	private void OnPlayerDied()
	{
		running = false;
		DeathScreenNode.ShowDeath(PlayerNode.Score, elapsed);
	}

	public override void _Process(double delta)
	{
		if (!running) return;
		elapsed += delta;
		HudNode.SetTime(elapsed);
	}
}
