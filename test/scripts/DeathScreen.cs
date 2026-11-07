using Godot;

public partial class DeathScreen : CanvasLayer
{
	[Export(PropertyHint.File, "*.tscn")] public string MenuScenePath = "res://scenes/main_menu.tscn";

	public override void _Ready()
	{
		Visible = false;

		GetNode<Button>("Control/VBoxContainer/Restart").Pressed += OnRestartPressed;
		GetNode<Button>("Control/VBoxContainer/Exit").Pressed += OnMenuPressed;
	}

	public void ShowDeath()
	{
		Visible = true;
		GetTree().Paused = true;
	}

	private void OnRestartPressed()
	{
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}

	private void OnMenuPressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile(MenuScenePath);
	}
}
