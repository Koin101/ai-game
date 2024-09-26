using Godot;
using System;

public partial class PortalSprite : Sprite2D
{
    private AnimationPlayer _animationPlayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{        
        _animationPlayer = GetNode<AnimationPlayer>("PlayerAnimation");
		_animationPlayer.Play("portal");
	}
}
