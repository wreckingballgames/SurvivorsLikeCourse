using Godot;

namespace Survivors.Player;

public partial class PlayerCharacter : CharacterBody3D
{
    [Export]
    private float MaxMoveSpeed { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Velocity = GetMovementVector() * MaxMoveSpeed;
        MoveAndSlide();
    }

    private Vector3 GetMovementVector()
    {
        float forwardMovement = Input.GetAxis("move_forward", "move_backward");
        float rightMovement = Input.GetAxis("strafe_left", "strafe_right");

        return new Vector3(rightMovement, 0.0f, forwardMovement).Normalized();
    }
}
