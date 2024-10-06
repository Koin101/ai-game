using Godot;
using System;

public partial class MainCharacter : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -380.0f;

	private AnimatedSprite2D _animatedSprite;
	private bool _isJumping = false;
	private bool _isChatting = false;
	private Vector2 _initialPosition;
	public bool climbing = false;
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("MainCharAnimation");
		// Store the character's initial position
		_initialPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{

		Console.WriteLine(climbing);
		if (_isChatting)
		{
			_animatedSprite.Play("Idle");
			return;
		}
		Vector2 velocity = Velocity;

		if (Input.IsActionJustPressed("TogglePlatformVisibility"))
		{
			var currentScene = GetTree().CurrentScene;
			currentScene.GetNode<TileMapLayer>("PlatformLayer").Visible = !currentScene.GetNode<TileMapLayer>("PlatformLayer").Visible;
		}

		// Add the gravity.
		if (!IsOnFloor() && !climbing)
		{
			velocity += GetGravity() * (float)delta;
		}
		else if (climbing)
		{
			velocity.Y = 0;

			if (Input.IsActionPressed("Move_Up"))
			{
				velocity.Y = -Speed;
			}

			if (Input.IsActionPressed("Move_Down"))
			{
				velocity.Y = Speed;
			}
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			_isJumping = true;
			_animatedSprite.Play("Jumping");
		}
		else if (IsOnFloor()) _isJumping = false;



		Vector2 direction = Input.GetVector("Move_Left", "Move_Right",
			"ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.X = direction.X * Speed;
			if (!_isJumping)
			{
				_animatedSprite.FlipH = direction.X switch
				{
					< 0 => true,
					> 0 => false,
					_ => _animatedSprite.FlipH
				};
				_animatedSprite.Play("Running");
			}
			else
			{
				_animatedSprite.FlipH = direction.X switch
				{
					< 0 => true,
					> 0 => false,
					_ => _animatedSprite.FlipH
				};
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (!_isJumping)
				_animatedSprite.Play("Idle");
		}

		Velocity = velocity;
		MoveAndSlide();
		if (Position.Y > GetTree().Root.Size.Y)
		{
			Position = _initialPosition;
		}
	}
	public void EnterChatMode()
	{
		_isChatting = true;
	}

	public void ExitChatMode()
	{
		_isChatting = false;
	}
}



