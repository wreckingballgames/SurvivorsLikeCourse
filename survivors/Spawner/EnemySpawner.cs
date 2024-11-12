using System;
using System.Collections.Generic;
using Godot;
using Survivors.Enemy;
using Survivors.Player;
using Range = Godot.Range;
using Utility = Survivors.Utilities.Utilities;

namespace Survivors.Spawner;

public partial class EnemySpawner : Node
{
    [Export]
    private PackedScene BasicEnemyScene { get; set; }
    [Export]
    private float SpawnRadius { get; set; }

    private Node _enemyContainer;

    private Timer _timer;

    private List<PlayerCharacter> _playerCharacters;
    private PlayerCharacter _targetPlayerCharacter;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("%Timer");
        _timer.Timeout += OnTimerTimeout;

        SceneTree sceneTree = GetTree();

        _enemyContainer = sceneTree.GetFirstNodeInGroup("enemy_container");
        if (_enemyContainer == null)
        {
            throw new Exception("No enemy container found in scene!");
        }

        Utility.CollectNodesInGroup(sceneTree, "player_characters", out _playerCharacters);
        if (_playerCharacters[0] != null)
        {
            _targetPlayerCharacter = _playerCharacters[0];
        }
        else
        {
            throw new Exception("No player characters found in scene!");
        }
    }

    private void OnTimerTimeout()
    {
        if (_targetPlayerCharacter == null)
        {
            return;
        }

        Vector3 randomDirection = Vector3.Right.Rotated(Vector3.Up, Utility.Rng.RandfRange(0.0f, Mathf.Tau));
        Vector3 spawnPosition = _targetPlayerCharacter.GlobalPosition + (randomDirection * SpawnRadius);

        BasicEnemy enemy = BasicEnemyScene.Instantiate() as BasicEnemy;
        if (enemy == null)
        {
            throw new Exception("Failed to instantiate enemy!");
        }
        _enemyContainer.AddChild(enemy, true);
        enemy.GlobalPosition = spawnPosition;
    }
}
