using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MeleeAttack", story: "[Self] MeleeAtack To [Target]", category: "Action", id: "f41a7fd7643fd18f319d16396009dc92")]
public partial class MeleeAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    [SerializeReference] public BlackboardVariable<float> Damage = new(10);
    [SerializeReference] public BlackboardVariable<float> Radius = new(3);
    [SerializeReference] public BlackboardVariable<float> Delay = new(2);

    private LayerMask targetLayer;
    private MonsterController monsterController;
    protected override Status OnStart()
    {
        if (monsterController == null)
        {
            monsterController = Self.Value.GetComponent<MonsterController>();
            targetLayer = LayerMask.GetMask("Playable");
        }
        Vector3 skillPos = Self.Value.transform.position.RemoveOne(RemoveDir.Y);
        monsterController.MonsterSkill(skillPos, Delay, Radius, () => MeleeAttack(skillPos));
        return Status.Running;
    }

    private void MeleeAttack(Vector3 skillPos)
    {
        foreach (var item in Physics.OverlapSphere(skillPos, Radius * 0.5f, targetLayer))
        {
            IDamagable damagable = item.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(Damage, EAtackElement.Normal);
            }
        }
    }
}

