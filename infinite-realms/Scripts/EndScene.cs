using Godot;
using System;

public partial class EndScene : Control
{
	// Called when the node enters the scene tree for the first time.
	private RichTextLabel _loreNode;
	private bool _animate = false;
	private float _animateSpeed;
	private bool _skip = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_loreNode = GetNode<RichTextLabel>("EndText");
		// The lower the faster
		_animateSpeed = 4.0f;
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
				GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
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
