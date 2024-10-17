using Godot;
using System;

public partial class Chest : Node2D
{
    public bool opened = false;
    public bool inRange = false;
    
    
    public void _on_chest_area_body_entered(MainCharacter body)
    {
        GD.Print("Chest entered");
        inRange = true;
    }
    
    public void _on_chest_area_body_exited(MainCharacter body)
    {
        GD.Print("Chest exited");
        inRange = false;
    }
    
    public override void _Process(double delta)
    {
        base._Process(delta);
        if(!opened && inRange && Input.IsActionPressed("chat"))
        {
            GD.Print("Chest opened");
            opened = true;
            GetNode<AnimatedSprite2D>("OpenCloseAnimation").Frame = 1;
        }
    }
}
