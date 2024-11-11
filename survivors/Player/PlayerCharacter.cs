using Godot;

namespace Survivors.Player;

public partial class PlayerCharacter : CharacterBody3D
{
    [Export]
    public float MaxMoveSpeed { get; private set; }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Velocity = GetMovementVector() * MaxMoveSpeed;
        MoveAndSlide();
        GD.Print(Velocity);
    }

    private Vector3 GetMovementVector()
    {
        float forwardMovement = Input.GetAxis("move_backward", "move_forward");
        float rightMovement = Input.GetAxis("strafe_left", "strafe_right");

        return new Vector3(rightMovement, 0.0f, forwardMovement).Normalized();
    }
}
