using Godot;
using System;

public partial class Samurai : MainCharacter
{



	public override void _Ready()
	{
		base._Ready();
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		// Store the character's initial position
		_initialPosition = Position;

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
}
