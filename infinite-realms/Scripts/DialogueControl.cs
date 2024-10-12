using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;

public partial class DialogueControl : Control
{
	Array<Dictionary> dialogue;
	public int currentDialogueID;
	RichTextLabel mainText;
	TextureRect moreTextIndicator;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		mainText = GetNode<RichTextLabel>("NinePatchRect/NPCText");
		moreTextIndicator = GetNode<TextureRect>("NinePatchRect/MoreTextIndicator");

		dialogue = LoadDialogue("res://Dialogue/dialogue.json");
		moreTextIndicator.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public static Array<Dictionary> LoadDialogue(string path)
	{
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		string text = file.GetAsText();
		var data = Json.ParseString(text);
		var content = data.AsGodotArray<Dictionary>();
		GD.Print(content);
		return content;
	}

	public void StartDialogue()
	{
		currentDialogueID = -1;
		NextScript();
	}

	public bool NextScript()
	{
		currentDialogueID++;
		if (currentDialogueID >= dialogue.Count)
		{
			return false;
		}
		mainText.Text = (string)dialogue[currentDialogueID]["text"];
		// Indicate that there is more text
		if (currentDialogueID + 1 >= dialogue.Count)
		{
			moreTextIndicator.Visible=false;
		}
		return true;
	}
	
	public void updateDialogue(string Line)
	{
		GD.Print(Line.Trim());
		mainText.Text = Line.Trim();
	}
}
