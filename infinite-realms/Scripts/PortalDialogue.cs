using Godot;
using System;

public partial class PortalDialogue : DialogueControl
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mainText = GetNode<RichTextLabel>("NinePatchRect/NPCText");
		moreTextIndicator = GetNode<TextureRect>("NinePatchRect/MoreTextIndicator");

		dialogue = LoadDialogue("res://Dialogue/portal.json");
		moreTextIndicator.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}
