using Godot;
using System;

public partial class Npc : CharacterBody2D
{
	public MainCharacter player;
	public DialogueControl chatBox;
	public Sprite2D keyIndicator;
	Area2D chatDetect;
	public bool playerInRange = false;
	public bool isChatting = false;
	private readonly System.Collections.Generic.Dictionary<String, AudioStreamPlayer2D> _sounds = new();


	public override void _Ready()
	{
		base._Ready();
		// Get Nodes
		player = GetNode<MainCharacter>("../MainCharacter");
		chatBox = GetNode<DialogueControl>("DialogueBox");
		chatDetect = GetNode<Area2D>("Chatdetection");
		keyIndicator = GetNode<Sprite2D>("KeyIndicator");
		var sounds = this.FindChildren("*", "AudioStreamPlayer2D");
		foreach (var sound in sounds)
		{
			_sounds.Add(sound.Name, (AudioStreamPlayer2D)sound);
		}

		chatBox.Visible = false;
		keyIndicator.Visible = false;
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
			if (!isChatting)
			{
				isChatting = true;
				keyIndicator.Visible = false;
				GD.Print("NOW ENTERING CHAT");
				player.EnterChatMode();
				chatBox.StartDialogue();
				chatBox.Visible = true;
				PlaySpeakingSound();
			} else
			{
				if (!chatBox.NextScript()) // NextScript returns false if the dialogue is exhausted
				{
					chatBox.Visible = false;
					player.ExitChatMode();
					isChatting = false;
				} else
				{
					PlaySpeakingSound();
				}
			}
		}


	}

	public void OnEnterChatDetection()
	{
		playerInRange = true;
		keyIndicator.Visible = true;
		GD.Print("Entered chat zone");
	}

	public void OnLeaveChatDetection() 
	{
		playerInRange = false;
		keyIndicator.Visible = false;
		GD.Print("Exited chat zone");
	}

	public void PlaySpeakingSound()
	{
		_sounds["SpeakingSound"].Play();
	}
}
