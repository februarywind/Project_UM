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
        Array.Fill(hits, new RaycastHit());
        Physics.SphereCastNonAlloc(transform.position, skillData.Radius, Vector3.up, hits, 1, skillData.TargetLayer);
        for (int i = 0; i < skillData.HitCount; i++)
        {
            foreach (var item in hits)
            {
                if (item.collider == null) continue;
                item.transform.GetComponent<IDamagable>().TakeDamage(skillData.Damage, EAtackElement.Electric);
            }
            yield return Utill.GetDelay(0.1f);
        }
        playerFSM.ChangeState(EPlayerState.Idle);
        SkillCoolTime();
    }
}
