using Godot;
using System;

public partial class Npc : CharacterBody2D
{
	public MainCharacter player;
	public bool playerInRange = false;
	public bool isChatting = false;

	public override void _Process(double delta)
	{
		base._Process(delta);
		player = GetNode<MainCharacter>("../MainCharacter");
		Area2D chatdetect = GetNode<Area2D>("Chatdetection");
		if (chatdetect.OverlapsBody(player))
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
