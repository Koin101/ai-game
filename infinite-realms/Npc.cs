using Godot;
using System;

public partial class Npc : CharacterBody2D
{
	public MainCharacter player;
	public Control chatBox;
	Area2D chatDetect;
	public bool playerInRange = false;
	public bool isChatting = false;

	public override void _Ready()
	{
		base._Ready();
		player = GetNode<MainCharacter>("../MainCharacter");
		chatBox = GetNode<Control>("DialogueBox");
		chatDetect = GetNode<Area2D>("Chatdetection");

		chatBox.Visible = false;
	}


	public override void _Process(double delta)
	{
		base._Process(delta);
		
		 
		if (chatDetect.OverlapsBody(player))
		{
			if (!playerInRange)
			{
				OnEnterChatDetection();
			}
			
		} else if (playerInRange)
		{
			OnLeaveChatDetection();
		}

		if (Input.IsActionJustPressed("chat") && playerInRange && player.IsOnFloor()) {
			GD.Print("NOW ENTERING CHAT");
			player.EnterChatMode();
			chatBox.Visible = true;
		}


	}

	public void OnEnterChatDetection()
	{
		playerInRange = true;
		GD.Print("Entered chat zone");
	}

	public void OnLeaveChatDetection() 
	{
		playerInRange = false;
		GD.Print("Exited chat zone");
	}
}
