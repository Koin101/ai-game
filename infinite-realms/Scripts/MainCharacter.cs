using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class MainCharacter : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -380.0f;

	public AnimatedSprite2D _animatedSprite;
	public bool _isJumping = false;
	public bool _isChatting = false;
	public readonly System.Collections.Generic.Dictionary<String, AudioStreamPlayer2D> _sounds = new();
	public Vector2 _initialPosition;
	public bool climbing = false;
	public override void _Ready()
	{
		//_animatedSprite = GetNode<AnimatedSprite2D>("MainCharAnimation");
		var sounds = this.FindChildren("*", "AudioStreamPlayer2D");
		foreach ( var sound in sounds )
		{
			_sounds.Add(sound.Name, (AudioStreamPlayer2D)sound);
		}
		
		// _animatedSprite = GetNode<AnimatedSprite2D>("MainCharAnimation");
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
		if (!IsOnFloor() && !climbing )
		{
			velocity += GetGravity() * (float)delta;
		}
		else if (climbing && _isJumping)
		{
			velocity += GetGravity() * (float)delta;
			if (Input.IsActionPressed("Move_Up"))
			{
				_isJumping = false;
				velocity.Y = -Speed;
			}

			if (Input.IsActionPressed("Move_Down"))
			{
				_isJumping = false;
				velocity.Y = Speed;
			}

		}
		else if (climbing)
		{
			velocity.Y = 0;

			if (Input.IsActionPressed("Move_Up"))
			{
				velocity.Y = -Speed;
				if (!_sounds["ClimbingSound"].Playing)
				{
					_sounds["ClimbingSound"].Play();
					_sounds["RunningSound"].Stop();
				}
				
			} else if (Input.IsActionPressed("Move_Down"))
			{
				velocity.Y = Speed;
				if (!_sounds["ClimbingSound"].Playing)
				{
					_sounds["ClimbingSound"].Play();
					_sounds["RunningSound"].Stop();
				}
			} else
			{
				_sounds["ClimbingSound"].Stop();
			}
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			_isJumping = true;
			_animatedSprite.Play("Jumping");
			_sounds["JumpSound"].Play();
			_sounds["RunningSound"].Stop();

		}
		else if (IsOnFloor()) _isJumping = false;



		Vector2 direction = Input.GetVector("Move_Left", "Move_Right",
			"ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.X = direction.X * Speed;
			if (!_isJumping && !climbing)
			{
				_animatedSprite.FlipH = direction.X switch
				{
					< 0 => false,
					> 0 => true,
					_ => _animatedSprite.FlipH
				};
				_animatedSprite.Play("Running");
				if (!_sounds["RunningSound"].Playing)
				{
					_sounds["RunningSound"].Play();
					_sounds["ClimbingSound"].Stop();
				}
				
			}
			else
			{
				_animatedSprite.FlipH = direction.X switch
				{
					< 0 => false,
					> 0 => true,
					_ => _animatedSprite.FlipH
				};
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (!_isJumping)
			{
				_animatedSprite.Play("Idle");
				_sounds["RunningSound"].Stop();
				_sounds["ClimbingSound"].Stop();
			}
				
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
