using Godot;
using System;

public partial class MainMenu : Control
{
	[Export(PropertyHint.File, "*.tscn")] public string GameScenePath = "res://scenes/game.tscn";

	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/PlayButton").Pressed += OnPlayPressed;
		GetNode<Button>("VBoxContainer/QuitButton").Pressed += OnQuitPressed;
	}

	private void OnPlayPressed()
	{
		GetTree().ChangeSceneToFile(GameScenePath);
	}

	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
