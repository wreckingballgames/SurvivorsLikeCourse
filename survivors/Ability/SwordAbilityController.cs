using System.Collections.Generic;
using System.Linq;
using Godot;
using Survivors.Enemy;
using Survivors.Player;
using Utility = Survivors.Utilities.Utilities;

namespace Survivors.Ability;

public partial class SwordAbilityController : Node
{
    [Export]
    private PackedScene SwordAbilityScene { get; set; }

    private Timer _timer;

    private List<BasicEnemy> _enemies;

    private PlayerCharacter _playerCharacter;
    private Node _abilityContainer;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("%Timer");
        _timer.Timeout += OnTimerTimeout;

        SceneTree sceneTree = GetTree();
        _playerCharacter = sceneTree.GetFirstNodeInGroup("player_characters") as PlayerCharacter;
        _abilityContainer = sceneTree.GetFirstNodeInGroup("ability_container");
    }

    private void OnTimerTimeout()
    {
        if (_playerCharacter == null)
        {
            return;
        }

        Utility.CollectNodesInGroup(GetTree(), "enemies", out _enemies);
        if (_enemies.Count == 0)
        {
            return;
        }

        SwordAbility swordAbilityInstance = SwordAbilityScene.Instantiate() as SwordAbility;
        _enemies = _enemies.Where((enemy) =>
            enemy.GlobalPosition.DistanceSquaredTo(_playerCharacter.GlobalPosition) <
                   swordAbilityInstance!.MaxRange
        ).ToList();

        if (_enemies.Count == 0)
        {
            return;
        }

        _enemies.Sort(SortByDistance);

        _abilityContainer.AddChild(swordAbilityInstance);
        swordAbilityInstance!.GlobalPosition = _enemies[0].GlobalPosition;
    }

    private int SortByDistance(Node3D a, Node3D b)
    {
        float aDistance = a.GlobalPosition.DistanceSquaredTo(_playerCharacter.GlobalPosition);
        float bDistance = b.GlobalPosition.DistanceSquaredTo(_playerCharacter.GlobalPosition);
        return aDistance.CompareTo(bDistance);
    }
}
