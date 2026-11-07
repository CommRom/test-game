using Godot;

public partial class Hud : CanvasLayer
{
	public void SetHp(int current, int max)
	{
		var bar = GetNode<ProgressBar>("%HPBar");
		bar.MaxValue = max;
		bar.Value = current;
	}
}
