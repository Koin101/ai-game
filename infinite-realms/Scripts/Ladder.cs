using Godot;
using System;
using GodotPlugins.Game;

public partial class Ladder : Area2D
{
	public readonly System.Collections.Generic.Dictionary<String, AudioStreamPlayer2D> _sounds = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var sounds = this.FindChildren("*", "AudioStreamPlayer2D");
		foreach (var sound in sounds)
		{
			_sounds.Add(sound.Name, (AudioStreamPlayer2D)sound);
		}
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
