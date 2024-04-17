using Godot;
using System;

public partial class UserInterface : Control
{
	Label fps;
	public override void _Ready()
	{
		fps = GetNode<Label>("FPSLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		fps.Text = $"FPS: {Engine.GetFramesPerSecond()}";
	}
}
