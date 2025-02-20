using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MagicAttack", story: "[Self] is MagicAttack This [Target]", category: "Action", id: "cf5a47cfb815d3b5591cc4983801e297")]
public partial class MagicAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    [SerializeReference] public BlackboardVariable<float> Damage = new(15);
    [SerializeReference] public BlackboardVariable<float> Radius = new(2);
    [SerializeReference] public BlackboardVariable<float> delay = new(2);

    private LayerMask targetLayer;

    private MonsterController monsterController;
    protected override Status OnStart()
    {
        // 상태에 첫 번째로 진입할 때 초기화
        if (targetLayer == 0)
        {
            targetLayer = LayerMask.GetMask("Playable");
            monsterController = Self.Value.GetComponent<MonsterController>();
        }

        Vector3 skillPos = Target.Value.transform.position.RemoveOne(RemoveDir.Y);
        monsterController.MonsterSkill(skillPos, delay, Radius, () => MagicAttack(skillPos));

        return Status.Running;
    }

    private void MagicAttack(Vector3 pos)
    {
        // 눈에 보이는 것 보다 범위가 크다. 왜 이럴까
        foreach (var item in Physics.OverlapSphere(pos, Radius, targetLayer))
        {
            IDamagable damagable = item.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(Damage, EAtackElement.Normal);
            }
        }
    }
}

