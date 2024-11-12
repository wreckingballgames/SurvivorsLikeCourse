using System;
using System.Collections.Generic;
using Godot;

namespace Survivors.Utilities;

public static class Utilities
{
    public static RandomNumberGenerator Rng { get; private set; } = new();

    public static void CollectNodesInGroup<T>(SceneTree sceneTree, string groupName, out List<T> collection) where T : Node
    {
        List<T> tempCollection = new();

        foreach (var node in sceneTree.GetNodesInGroup(groupName))
        {
            if (node is T nodeT)
            {
                tempCollection.Add(nodeT);
            }
        }

        collection = tempCollection;
    }

    // Get the weight for a lerp using the formula suggested by Firebelley Games
    public static float GetNaturalWeight(float delta, float factor)
    {
        return 1.0f - MathF.Exp(-delta * factor);
    }
}
