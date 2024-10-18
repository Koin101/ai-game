using Godot;
using System;
using System.Linq;

public partial class Portal : Area2D
{
	[Export] public string Password { get; set; }

	private string FILE_PATH = "res://Scenes/Levels/Level";
	private int LAST = 3;
	public MainCharacter player;
	public bool portalPermission = false;
	public bool playerInRange = false;
	public bool isChatting = false;
	PortalDialogue portalBox;
	public LineEdit userInput;
	private Sprite2D keyIndicator;
	public void OnBodyEntered(Node2D body)
	{

		Console.WriteLine("Body entered");
		if (body.IsInGroup("Player"))
		{
			player = (MainCharacter)body;
			playerInRange = true;
			if (!portalPermission)
			{
				keyIndicator.Visible = true;
			}
		}
	}

	public void OnBodyExited(Node2D body)
	{
		keyIndicator.Visible = false;
		playerInRange = false;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		keyIndicator = GetNode<Sprite2D>("KeyIndicator");
		portalBox = GetNode<PortalDialogue>("PortalDialogue");
		userInput = GetNode<LineEdit>("LineEdit");
		keyIndicator.Visible = false;
		userInput.Visible = false;
		portalBox.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (playerInRange && portalPermission) 
		{
			TeleportPlayer();
		}

		if (Input.IsActionJustPressed("chat") && playerInRange && player.IsOnFloor())
		{
			if (!isChatting)
			{
				isChatting = true;
				keyIndicator.Visible = false;
				GD.Print("NOW ENTERING CHAT");
				player.EnterChatMode();
				portalBox.StartDialogue();
				// Ask for password
				portalBox.Visible = false;
				userInput.Visible = true;
				userInput.GrabFocus();
			}
			else if (!userInput.HasFocus())
			{
				userInput.Visible = false;
				if (!portalBox.NextScript()) // NextScript returns false if the dialogue is exhausted
				{
					portalBox.Visible = false;
					player.ExitChatMode();
					isChatting = false;
				}
			}
		}
	}

	public void TeleportPlayer()
	{
		SoundPlayer.Play("PortalSound");
		String currentScene = GetTree().CurrentScene.SceneFilePath;
		int currentLevel = String.Join("", currentScene.Where(char.IsDigit)).ToInt();
		currentLevel++;
		if (currentLevel > LAST)
		{
			GetTree().ChangeSceneToFile("res://Scenes/EndScene.tscn");
		}

		currentScene = FILE_PATH + currentLevel.ToString() + ".tscn";

		GetTree().CallDeferred("change_scene_to_file", currentScene);
	}

	public void OnLineEditTextSubmitted(string text)
	{
		if (text.ToLower().Contains(Password ?? ""))
		{
			portalPermission = true;
		} else
		{
			portalBox.Visible = true;
			userInput.Visible = false;
		}
		userInput.Clear();
	}
}
