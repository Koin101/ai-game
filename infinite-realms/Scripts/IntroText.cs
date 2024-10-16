using Godot;
using System;

public partial class IntroText : Control
{
	private RichTextLabel _loreNode;
	private bool _animate = false;
	private float _animateSpeed;
	private bool _skip = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_loreNode = GetNode<RichTextLabel>("StoryText1");
		_animateSpeed = 6.0f;
		_animate = true;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_animate) AnimateText();



		if(_loreNode.VisibleRatio >= 1.0f && _skip)
		{
			_animate = false;
			if (Input.IsActionPressed("SkipScene"))
			{
				GetTree().ChangeSceneToFile("res://Scenes/Levels/Level1.tscn");
			}
		}
		
		if (Input.IsActionPressed("SkipScene") && !_skip)
		{
			_loreNode.VisibleRatio = 1.0f;
			_skip = true;
		}
	}


	private void AnimateText()
	{
		_loreNode.VisibleRatio += (float) (1.0 / _loreNode.Text.Length / _animateSpeed);
	}
}
