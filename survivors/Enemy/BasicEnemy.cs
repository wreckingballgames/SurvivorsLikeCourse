using System.Collections.Generic;
using Godot;
using Survivors.Player;
using Utility = Survivors.Utilities.Utilities;

namespace Survivors.Enemy;

public partial class BasicEnemy : CharacterBody3D
{
    [Export]
    private float MaxMoveSpeed { get; set; }

    private List<PlayerCharacter> _playerCharacters;
    private PlayerCharacter _targetPlayerCharacter;

    private Area3D _area3D;

    public override void _Ready()
    {
        Utility.CollectNodesInGroup(GetTree(), "player_characters", out _playerCharacters);
        if (_playerCharacters[0] != null)
        {
            _targetPlayerCharacter = _playerCharacters[0];
        }

        _area3D = GetNode<Area3D>("%Area3D");
        _area3D!.AreaEntered += OnAreaEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = MaxMoveSpeed * GetDirectionToPlayer();
        MoveAndSlide();
        if (_targetPlayerCharacter != null)
        {
            LookAt(_targetPlayerCharacter.GlobalPosition);
        }
    }

    private Vector3 GetDirectionToPlayer()
    {
        return _targetPlayerCharacter != null
            ? (_targetPlayerCharacter.GlobalPosition - GlobalPosition).Normalized()
            : Vector3.Zero;
    }

    private void OnAreaEntered(Area3D area)
    {
        QueueFree();
    }
}
