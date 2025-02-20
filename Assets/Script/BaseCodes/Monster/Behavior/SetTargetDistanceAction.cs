using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTargetDistance", story: "[Distance] [Self] To [Target]", category: "Action", id: "5c901c7609c578d2ce90b4c51c2f6792")]
public partial class SetTargetDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Distance;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        Distance.Value = Vector3.Distance(Self.Value.transform.position, Target.Value.transform.position);
        return Status.Running;
    }
}

