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
	int currentLevel;
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
		String currentScene = GetTree().CurrentScene.SceneFilePath;
		currentLevel = String.Join("", currentScene.Where(char.IsDigit)).ToInt();
		keyIndicator.Visible = false;
		userInput.Visible = false;
		portalBox.Visible = false;

		if (Password == null)
		{
			portalPermission = true;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (playerInRange && portalPermission) 
		{
			TeleportPlayer(currentLevel + 1);
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

	public void TeleportPlayer(int level)
	{
		SoundPlayer.Play("PortalSound");
		if (level > LAST)
		{
			GetTree().ChangeSceneToFile("res://Scenes/EndScene.tscn");
		}

		String nextScene = FILE_PATH + level.ToString() + ".tscn";

		GetTree().CallDeferred("change_scene_to_file", nextScene);
	}

	public void OnLineEditTextSubmitted(string text)
	{
		GD.Print(currentLevel);
		if (text.ToLower().Contains(Password ?? ""))
		{
			portalPermission = true;
		} else
		{
			portalBox.Visible = true;
			userInput.Visible = false;
			if (currentLevel != 1)
			{
				// If you get the password wrong you are sent back to level 1 if you are not already in level 1
				TeleportPlayer(1);
			}
		}
		userInput.Clear();
	}
}
