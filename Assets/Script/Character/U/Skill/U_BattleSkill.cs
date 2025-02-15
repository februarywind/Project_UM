using System;
using System.Collections;
using UnityEngine;

public class U_BattleSkill : BattleSkillBase
{
    private RaycastHit[] hits;

    private void Awake()
    {
        hits = new RaycastHit[skillData.MaxTarget];
    }

    public override void BattleSkillActivate()
    {
        StartCoroutine(DashSkill());
    }

    IEnumerator DashSkill()
    {
        Vector3 skillDir = playerController.IsInput ? playerController.InputDir : transform.forward;
        int indexLength = Physics.SphereCastNonAlloc(transform.position, skillData.Radius, skillDir, hits, skillData.Range, skillData.TargetLayer);
        for (int i = 0; i < indexLength; i++)
        {
            hits[i].transform.GetComponent<IDamagable>().TakeDamage(stat.AttackPower * skillData.DamageRatio, EAtackElement.Electric);
        }

        playerController.characterController.excludeLayers += skillData.TargetLayer;
        playerController.characterController.Move(skillDir * skillData.Range);
        playerController.characterController.excludeLayers -= skillData.TargetLayer;

        yield return Utill.GetDelay(skillData.Delay);

        playerFSM.ChangeState(EPlayerState.Idle);

        SkillCoolTime();
    }

    private void OnDrawGizmos()
    {
        // 시작점 원 (SphereCast의 초기 위치)
        Gizmos.DrawWireSphere(transform.position, skillData.Radius);

        // 끝점 원 (SphereCast가 도달할 위치)
        Gizmos.DrawWireSphere(transform.position + transform.forward * skillData.Range, skillData.Radius);
    }
}
