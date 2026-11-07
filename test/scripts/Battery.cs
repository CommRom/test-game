using Godot;
using System;

public partial class Battery : Area2D
{
	public override void _Ready(){
		GD.Print("Ready");
	}
	private void _on_body_entered(Node2D body)
	{
		if (body is Player player){
		GD.Print("Player entered");
		player.Heal(10);
		QueueFree();
		}
	}
}
