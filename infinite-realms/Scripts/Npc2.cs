using Godot;
using System;

public partial class Npc2 : CharacterBody2D
{
	public String reaction = "";
	public string llmDialogue = "";
	public bool llmAvailable = true;
	public MainCharacter player;
	public DialogueControl2 chatBox;
	public LineEdit userInput;
	public Sprite2D keyIndicator;
	Area2D chatDetect;
	public bool playerInRange = false;
	public bool isChatting = false;
	private readonly System.Collections.Generic.Dictionary<String, AudioStreamPlayer2D> _sounds = new();

	public override void _Ready()
	{
		//GD.Print(Flags.GetFlag("failed2"));
		base._Ready();
		// Get Nodes
		player = GetNode<MainCharacter>("../Grandpa");
		chatBox = GetNode<DialogueControl2>("DialogueBox");
		chatDetect = GetNode<Area2D>("Chatdetection");
		keyIndicator = GetNode<Sprite2D>("KeyIndicator");
		var sounds = this.FindChildren("*", "AudioStreamPlayer2D");
		userInput = GetNode<LineEdit>("LineEdit");
		foreach (var sound in sounds)
		{
			_sounds.Add(sound.Name, (AudioStreamPlayer2D)sound);
		}

		chatBox.Visible = false;
		keyIndicator.Visible = false;
		userInput.Visible = false;
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

		if (Input.IsActionJustPressed("chat") && playerInRange && player.IsOnFloor() && llmAvailable) {
			if (!isChatting)
			{
				isChatting = true;
				keyIndicator.Visible = false;
				GD.Print("NOW ENTERING CHAT");
				player.EnterChatMode();
				chatBox.StartDialogue();
				chatBox.Visible = true;
				PlaySpeakingSound();
			} else if(chatBox.currentDialogueID == 1)
			{
				chatBox.Visible = false;
				userInput.Visible = true;
				userInput.GrabFocus();
			} else
			{
				userInput.Visible = false;
				if (!chatBox.NextScript()) // NextScript returns false if the dialogue is exhausted
				{
					chatBox.Visible = false;
					player.ExitChatMode();
					isChatting = false;
				} else
				{
					GD.Print("yessir");
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
	
	public void OnLineEditTextSubmitted(string text)
	{
		GD.Print(text);
		userInput.Visible = false;
		userInput.Clear();
		chatBox.Visible = true;
		chatBox.NextScript();
		
		if(checkBlacklist(text))
		{
			chatBox.updateDialogue("Asking it so directly, how dishonorable");
		}
		else
		{
			reaction = "";
			llmDialogue = "";
			userInput.Call("start_wizard", text);
			llmAvailable = false;
		}
	}
	
	public void OnGdllamaUpdated(string text)
	{
		//GD.Print(text);
		if(reaction.Contains("response\":"))
		{
			llmDialogue += text;
			llmDialogue = llmDialogue.Split('<')[0];
			llmDialogue = llmDialogue.Replace("}","");
			//reaction = reaction.Substring(reaction.IndexOf("response") + 2);
			chatBox.updateDialogue(llmDialogue);
		}
		reaction += text;
		reaction = reaction.Replace(" :", ":");
	}
	
	public void OnGdllamaAvailable(bool finished)
	{
		llmAvailable = true;
	}
	
	private bool checkBlacklist(string text)
	{
		foreach(string each in chatBox.blacklist)
		{
			if(text.ToLower().Contains(each))
			{
				return true;
			}
		}
		return false;
	}
}
