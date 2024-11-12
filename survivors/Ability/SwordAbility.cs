using Godot;

namespace Survivors.Ability;

public partial class SwordAbility : Node3D
{
    [Export]
    public float MaxRange { get; private set; }

    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
        _animationPlayer.AnimationFinished += OnAnimationFinished;
    }

    private void OnAnimationFinished(StringName animationName)
    {
        // Being extra careful
        if (animationName.ToString() == "swing")
        {
            QueueFree();
        }
    }
}
