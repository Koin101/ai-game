using Godot;
using System;

public partial class PauseMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_pauseExit_button_pressed()
	{
		GetTree().Quit();
	}
	
	public void _on_pauseReturn_button_pressed()
	{
		this.Visible = false;
	}
}
