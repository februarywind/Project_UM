using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CurHpPerCheck", story: "[Self] [CurHpPer] Check", category: "Action", id: "7a50c1ac2cbe87ebae25c052d5a52656")]
public partial class CurHpPerCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> CurHpPer;

    private MonsterBase monsterBase;

    protected override Status OnStart()
    {
        if (monsterBase == null)
        {
            monsterBase = Self.Value.GetComponent<MonsterBase>();
        }
        CurHpPer.Value = monsterBase.CurHp / monsterBase.MaxHp * 100f;
        return Status.Running;
    }
}

