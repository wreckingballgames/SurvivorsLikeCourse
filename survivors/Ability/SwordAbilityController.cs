using Godot;
using Survivors.Player;

namespace Survivors.Ability;

public partial class SwordAbilityController : Node
{
    [Export]
    private PackedScene SwordAbilityScene { get; set; }

    private Timer _timer;

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
        Node3D swordAbilityInstance = SwordAbilityScene.Instantiate() as Node3D;
        _abilityContainer.AddChild(swordAbilityInstance);
        swordAbilityInstance!.GlobalPosition = _playerCharacter.GlobalPosition;
    }
}
