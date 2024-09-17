using Godot;
using System;

public partial class Level1 : Node2D
{
	public event Action RenderLevelFinished;
	
	
	public void StartRender()
	{
		var renderScene = GetNode<RenderScene>("RenderScene");

		renderScene.Call("render", GetNode<TileMapLayer>("PlatformLayer"));
		
	}
	
	public override void _Ready()
	{
		
		Console.WriteLine("Level 1 loaded");
		var renderScene = GetNode<RenderScene>("RenderScene");
		renderScene.RenderFinished += _on_Render_finished;
	}
	
	private void _on_Render_finished()
	{
		RenderLevelFinished?.Invoke();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
