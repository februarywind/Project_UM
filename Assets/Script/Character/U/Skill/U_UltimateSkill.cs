using System;
using System.Collections;
using UnityEngine;

public class U_UltimateSkill : UltimateSkillBase
{
    private RaycastHit[] hits;

    private void Awake()
    {
        hits = new RaycastHit[skillData.MaxTarget];
    }

    public override void UltimateSkillActivate()
    {
        StartCoroutine(U_Ultimate());
    }

    IEnumerator U_Ultimate()
    {
        int indexLength = Physics.SphereCastNonAlloc(transform.position, skillData.Radius, Vector3.up, hits, 1, skillData.TargetLayer);
        for (int i = 0; i < skillData.HitCount; i++)
        {
            for (int j = 0; j < indexLength; j++)
            {
                hits[j].transform.GetComponent<IDamagable>().TakeDamage(stat.AttackPower * skillData.DamageRatio, EAtackElement.Electric);
            }
            yield return Utill.GetDelay(0.1f);
        }
        playerFSM.ChangeState(EPlayerState.Idle);
        SkillCoolTime();
    }
}
