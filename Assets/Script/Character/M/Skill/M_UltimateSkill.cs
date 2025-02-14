using System;
using System.Collections;
using UnityEngine;

public class M_UltimateSkill : UltimateSkillBase
{
    private RaycastHit[] hits;

    private void Awake()
    {
        hits = new RaycastHit[skillData.MaxTarget];
    }
    public override void UltimateSkillActivate()
    {
        StartCoroutine(HealSkill());
    }

    IEnumerator HealSkill()
    {
        yield return Utill.GetDelay(skillData.Delay);
        StartCoroutine(UltimateSkill(transform.position));
        playerFSM.ChangeState(EPlayerState.Idle);
    }

    IEnumerator UltimateSkill(Vector3 pos)
    {
        for (int i = 0; i < skillData.HitCount; i++)
        {
            Array.Fill(hits, new RaycastHit());
            Physics.SphereCastNonAlloc(pos, skillData.Radius, Vector3.up, hits, skillData.Range, skillData.TargetLayer);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider == null) continue;
                if (1 << hit.transform.gameObject.layer == LayerMask.GetMask("Monster"))
                {
                    hit.transform.GetComponent<IDamagable>().TakeDamage(skillData.Damage, EAtackElement.Normal);
                }
                else
                {
                    hit.transform.GetComponent<PlayerController>().StatController.Stat.CurHp += skillData.Damage;
                    DamagePopUpManager.instance.ShowDamagePopUp(hit.transform.position, $"{skillData.Damage}", Color.green);
                }
            }
            yield return Utill.GetDelay(0.5f);
        }
    }
}
