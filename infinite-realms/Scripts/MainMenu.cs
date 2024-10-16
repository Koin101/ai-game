using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// GetNode<Button>("VBoxContainer/StartButton").GrabFocus();
	}

	private void _on_start_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/IntroText.tscn");
	}
	
	private void _on_options_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/OptionsMenu.tscn");

	}
	
	private void _on_exit_button_pressed()
	{
		GetTree().Quit();
	}
}
