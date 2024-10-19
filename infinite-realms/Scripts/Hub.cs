using Godot;
using System;

public partial class Hub : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SoundPlayer.StopAll();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
