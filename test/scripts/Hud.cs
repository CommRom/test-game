using Godot;

public partial class Hud : CanvasLayer
{
	[Export] public Label ScoreLabel;
	[Export] public Label TimeLabel;
	
	
	public void SetScore(int score)
	{
		ScoreLabel.Text = $"Score: {score}";
	}

	public void SetTime(double seconds)
	{
		TimeLabel.Text = $"Time: {FormatTime(seconds)}";
	}

	public static string FormatTime(double seconds)
	{
		int total = (int)seconds;
		return $"{total / 60:00}:{total % 60:00}";   // e.g. 01:07
	}
	
	public void SetHp(int current, int max)
	{
		var bar = GetNode<ProgressBar>("%HPBar");
		bar.MaxValue = max;
		bar.Value = current;
	}
}
