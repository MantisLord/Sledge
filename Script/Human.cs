using Godot;

public partial class Human : Node3D
{
    AnimationPlayer anim;
    public override void _Ready()
    {
        anim = GetNode<AnimationPlayer>("AnimationPlayer");
        if (Name == "Human")
            anim.Play("Walk");
    }

	public override void _Process(double delta)
	{
	}
}
