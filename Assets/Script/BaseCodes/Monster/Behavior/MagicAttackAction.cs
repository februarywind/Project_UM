using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MagicAttack", story: "[Self] is MagicAttack This [Target]", category: "Action", id: "cf5a47cfb815d3b5591cc4983801e297")]
public partial class MagicAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    [SerializeReference] public BlackboardVariable<float> Count = new(3);
    [SerializeReference] public BlackboardVariable<float> NextComboDelay = new(0.5f);

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
        monsterController.CoroutineAgent(MagicAttack());

        return Status.Running;
    }

    private void MagicAttack(Vector3 pos)
    {
        // Shpere 오브젝트의 Scale이 1일 때 반지름이 0.5임 OverlapSphere의 Radius는 반지름을 입력 해야 함 스케일이 1인 Shpere와 같은 크기의 Overlap을 원한다면 0.5를 써야함
        foreach (var item in Physics.OverlapSphere(pos, Radius * 0.5f, targetLayer))
        {
            IDamagable damagable = item.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(Damage, EAtackElement.Normal, Self.Value.transform);
            }
        }
    }

    IEnumerator MagicAttack()
    {
        for (int i = 0; i < Count; i++)
        {
            Vector3 skillPos = Target.Value.transform.position.RemoveOne(RemoveDir.Y);
            monsterController.MonsterSkill(skillPos, delay, Radius, () => MagicAttack(skillPos));
            yield return Util.GetDelay(NextComboDelay);
        }
    }
}

