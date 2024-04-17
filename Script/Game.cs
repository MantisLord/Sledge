using Godot;
public partial class Game : Node
{
    public override void _Ready()
    {
        base._Ready();
    }
    public void ChangeScene(string sceneName)
    {
        GetTree().ChangeSceneToFile($"res://Scene/{sceneName}.tscn");
    }
}
