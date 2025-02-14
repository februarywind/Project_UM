using System;
using System.Collections;
using UnityEngine;

public class M_BattleSkill : BattleSkillBase
{
    private RaycastHit[] hits;

    private void Awake()
    {
        hits = new RaycastHit[skillData.MaxTarget];
    }

    public override void BattleSkillActivate()
    {
        StartCoroutine(HealSkill());
    }

    IEnumerator HealSkill()
    {
        yield return Utill.GetDelay(skillData.Delay);
        Array.Fill(hits, new RaycastHit());
        Physics.SphereCastNonAlloc(transform.position, skillData.Radius, Vector3.up, hits, skillData.Range, skillData.TargetLayer);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider == null) continue;
            hit.transform.GetComponent<PlayerController>().StatController.Stat.CurHp += skillData.Damage;
            DamagePopUpManager.instance.ShowDamagePopUp(hit.transform.position, $"{skillData.Damage}", Color.green);
        }
        yield return Utill.GetDelay(skillData.Delay);
        playerFSM.ChangeState(EPlayerState.Idle);
    }
}
