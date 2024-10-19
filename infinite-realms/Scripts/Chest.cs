using Godot;
using System;

public partial class Chest : Node2D
{
	public bool opened = false;
	public bool inRange = false;
	public MainCharacter player;
	public Area2D chestArea;
	public Sprite2D keyIndicator;
	private readonly System.Collections.Generic.Dictionary<String, AudioStreamPlayer2D> _sounds = new();
	private RichTextLabel _hintText;
	private float _animateSpeed = 4.0f;
	private bool _startAnimation = false;
	[Export] public string hintText = "Ask for a part and not the whole";
	public void _on_chest_area_body_entered(MainCharacter body)
	{
		GD.Print("Chest entered");
		inRange = true;
		if (!opened) {
			keyIndicator.Visible = true;
		}
	}
	
	public void _on_chest_area_body_exited(MainCharacter body)
	{
		GD.Print("Chest exited");
		inRange = false;
		keyIndicator.Visible = false;
	}

	public override void _Ready()
	{
		base._Ready();
		player = GetTree().GetFirstNodeInGroup("Player") as MainCharacter;
		chestArea = GetNode<Area2D>("ChestArea");
		keyIndicator = GetNode<Sprite2D>("KeyIndicator");
		keyIndicator.Visible = false;
		_hintText = GetNode<RichTextLabel>("HintText");
		_hintText.SetText(hintText);
		var sounds = this.FindChildren("*", "AudioStreamPlayer2D");
		foreach (var sound in sounds)
		{
			_sounds.Add(sound.Name, (AudioStreamPlayer2D)sound);
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (chestArea.OverlapsBody(player) && Input.IsActionPressed("chat") && !opened)
		{
			GD.Print("chest opened");
			opened = true;
			keyIndicator.Visible = false;
			GetNode<AnimatedSprite2D>("OpenCloseAnimation").Frame = 1;
			_sounds["ChestSound"].Play();
			_startAnimation = true;
		}
		
		if(_startAnimation) AnimateText();
	}
	
	private void AnimateText()
	{
		_hintText.VisibleRatio += (float) (1.0 / _hintText.Text.Length / _animateSpeed);
	}
}
