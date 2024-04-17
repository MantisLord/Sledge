using Godot;

public partial class Player : RigidBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
    private const float MOUSE_SENSITIVITY = 0.15f;
    private const float MAX_ANGLE_VIEW = 60f;
    private const float MIN_ANGLE_VIEW = -40f;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	private Node3D head;
	private Camera3D cam;
    private ShapeCast3D shape;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        cam = GetNode<Camera3D>("Head/Camera3D");
		head = GetNode<Node3D>("Head");
        shape = GetNode<ShapeCast3D>("ShapeCast3D");
        base._Ready();
    }

    public override void _Process(double delta)
    {
		base._Process(delta);
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.GetType() == typeof(InputEventMouseMotion) && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            InputEventMouseMotion inputEventMouseMotion = (InputEventMouseMotion)@event;
            head.RotateY(Mathf.DegToRad(-inputEventMouseMotion.Relative.X * MOUSE_SENSITIVITY));
            cam.RotateX(Mathf.DegToRad(-inputEventMouseMotion.Relative.Y * MOUSE_SENSITIVITY));
            cam.Rotation = new Vector3(Mathf.Clamp(cam.Rotation.X, Mathf.DegToRad(MIN_ANGLE_VIEW), Mathf.DegToRad(MAX_ANGLE_VIEW)), 0f, 0f);
        }
        base._UnhandledInput(@event);
    }

    private bool OnGround()
    {
        if (shape.IsColliding())
            return true;
        return false;
    }

    public override void _PhysicsProcess(double delta)
    {
        var inputDir = Input.GetVector("left", "right", "forward", "backward");
        var direction = (head.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        ApplyCentralImpulse(direction.Normalized() * 0.04f);

        if (OnGround())
        {
            ApplyCentralImpulse(direction.Normalized() * 0.08f);

            if (Input.IsActionPressed("ui_accept"))
            {
                LinearVelocity = new Vector3(LinearVelocity.X, 7, LinearVelocity.Z);
            }
        }

        base._PhysicsProcess(delta);
    }
}
