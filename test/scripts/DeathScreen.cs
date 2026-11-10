using Godot;

public partial class DeathScreen : CanvasLayer
{
	[Export(PropertyHint.File, "*.tscn")] public string MenuScenePath = "res://scenes/main_menu.tscn";

	[Export] public Label ScoreLabel;
	[Export] public Label TimeLabel;
	
	public override void _Ready()
	{
		Visible = false;

		GetNode<Button>("Control/VBoxContainer/Restart").Pressed += OnRestartPressed;
		GetNode<Button>("Control/VBoxContainer/Exit").Pressed += OnMenuPressed;
	}

	public void ShowDeath(int score, double seconds)
	{
		ScoreLabel.Text = $"Score: {score}";
		TimeLabel.Text = $"Time survived: {Hud.FormatTime(seconds)}";
		
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
