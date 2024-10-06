using Godot;
using System;
using GodotPlugins.Game;

public partial class Ladder : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void _on_ladder_body_entered(MainCharacter body)
	{
		if(!body.climbing)
		{
			body.climbing = true;
		}
	}
	
	void _on_ladder_body_exited(MainCharacter body)
	{
		if(body.climbing)
		{
			body.climbing = false;
		}
	}
}
