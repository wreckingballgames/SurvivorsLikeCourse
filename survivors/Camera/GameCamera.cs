using System.Collections.Generic;
using Godot;
using Survivors.Player;
using Utility = Survivors.Utilities.Utilities;

namespace Survivors.Camera;

public partial class GameCamera : Camera3D
{
	[Export]
	// Must not share Y-axis precisely with _activePlayerCharacter (i.e., X and/or Z must be non-zero)
	private Vector3 PlayerCharacterOffset { get; set; }

	private SceneTree _sceneTree;
	private List<PlayerCharacter> _playerCharacters;
	private PlayerCharacter _activePlayerCharacter;

	public override void _Ready()
	{
		MakeCurrent();
		_sceneTree = GetTree();
		Utility.CollectNodesInGroup(_sceneTree, "player_characters", out _playerCharacters);
		_activePlayerCharacter = _playerCharacters[0];
	}

	public override void _Process(double delta)
	{
		GlobalPosition = _activePlayerCharacter.GlobalPosition + PlayerCharacterOffset;
		LookAt(_activePlayerCharacter.GlobalPosition);
	}
}
