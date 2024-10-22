using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;

public partial class DialogueControl2 : Control
{
	public Array<Dictionary> dialogue;
	public int currentDialogueID;
	public RichTextLabel mainText;
	public TextureRect moreTextIndicator;
	public string[] blacklist = {"password", "pw", "secret"};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		mainText = GetNode<RichTextLabel>("NinePatchRect/NPCText");
		moreTextIndicator = GetNode<TextureRect>("NinePatchRect/MoreTextIndicator");

		dialogue = LoadDialogue("res://Dialogue/dialogue2.json");
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
		if(Flags.GetFlag("1Obtained") || Flags.GetFlag("failed2"))
		{
			NextScript();
		}
		else
		{
			NextScript();
			updateDialogue("First talk to the grand wizard\n You are not ready to talk to me.");
			currentDialogueID = dialogue.Count;
		}
	}

	public bool NextScript()
	{
		currentDialogueID++;
		if(mainText.Text.Contains("FOXTROT"))
		{
			currentDialogueID++;
		}
		
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
