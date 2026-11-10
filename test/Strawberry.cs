using Godot;
using System;

public partial class Strawberry : Area2D
{
	public override void _Ready(){
	}
	private void _on_body_entered(Node2D body)
	{
		if (body is Player player){
		player.AddPoints(1);
		QueueFree();
		}
	}
}
