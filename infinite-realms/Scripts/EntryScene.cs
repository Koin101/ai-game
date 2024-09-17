using Godot;
using System;
using System.Linq;

public partial class EntryScene : Node
{
	private bool _render = false;

	private string[] _args;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_args = OS.GetCmdlineArgs();
		if(_args.Contains("--render"))
			_render = true;

		if (_render)
		{
			var scene = ResourceLoader.Load<PackedScene>("res://Scenes/Level_1.tscn");
			var instance = (Level1) scene.Instantiate();
			
			instance.RenderLevelFinished += _on_Render_finished;
			RenderingServer.FramePostDraw += instance.StartRender;

		}
		else
			GetTree().ChangeSceneToFile("res://Scenes/main_game.tscn");
	}
	
	private void _on_Render_finished()
	{
		Console.WriteLine("Render Completed, unloading");
		GetTree().Quit();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
