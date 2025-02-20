using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MeleeAttack", story: "[Self] MeleeAtack To [Target]", category: "Action", id: "f41a7fd7643fd18f319d16396009dc92")]
public partial class MeleeAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        Target.Value.GetComponent<PlayableStatController>().TakeDamage(5, EAtackElement.Normal);
        return Status.Running;
    }
}

