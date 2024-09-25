using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;

public partial class Dialogue : RichTextLabel
{
    // Called when the node enters the scene tree for the first time.
    Array<Dictionary> dialogue;
	int currentDialogueID;

	public override void _Ready()
	{
		base._Ready();
		dialogue = LoadDialogue("res://Dialogue/dialogue.json");
		currentDialogueID = -1;
		NextScript();

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static Array<Dictionary> LoadDialogue(string path)
	{
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		string text = file.GetAsText();
		var data = Json.ParseString(text);
		var content = data.AsGodotArray<Dictionary>();
		GD.Print(content);
		return content;
	}

	private void NextScript()
	{

		currentDialogueID++;
		if (currentDialogueID >= dialogue.Count)
		{
			return;
		}
		this.Text = (string)dialogue[currentDialogueID]["text"];

	}
}
