using Godot;
using System;
using System.Linq;

public partial class Portal : Area2D
{
    private string FILE_PATH = "res://Scenes/Levels/Level";
    private int LAST = 2;
    public void OnBodyEntered(Node2D body)
    {
        if(body.IsInGroup("Player"))
        {
            String currentScene = GetTree().CurrentScene.SceneFilePath;
            int currentLevel = String.Join("", currentScene.Where(char.IsDigit)).ToInt();
            currentLevel++;
            if (currentLevel > LAST)
            {
                currentLevel = 1;
            }
            
            currentScene = FILE_PATH + currentLevel.ToString() + ".tscn";
            
            GetTree().CallDeferred("change_scene_to_file", currentScene);
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
