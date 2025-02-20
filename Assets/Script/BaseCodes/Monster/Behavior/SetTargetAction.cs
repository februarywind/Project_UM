using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTarget", story: "Find [Target] To [Players]", category: "Action", id: "2678a0799755917fc3266bff5ba0ff0d")]
public partial class SetTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Players;

    protected override Status OnStart()
    {
        FindActive();
        return Status.Running;
    }

    private bool FindActive()
    {
        var actives = Players.Value.Where(x => x.activeSelf).ToArray();
        if (actives.Length == 0)
        {
            return false;
        }
        Target.Value = actives[UnityEngine.Random.Range(0, actives.Length)];
        return true;
    }
}

