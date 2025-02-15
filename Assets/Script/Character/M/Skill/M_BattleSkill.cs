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
        float healValue = stat.AttackPower * skillData.DamageRatio;
        yield return Utill.GetDelay(skillData.Delay);
        int indexLength = Physics.SphereCastNonAlloc(transform.position, skillData.Radius, Vector3.up, hits, skillData.Range, skillData.TargetLayer);
        for (int i = 0; i < indexLength; i++)
        {
            hits[i].transform.GetComponent<PlayerController>().StatController.Stat.CurHp += healValue;
            DamagePopUpManager.instance.ShowDamagePopUp(hits[i].transform.position, $"{healValue}", Color.green);
        }
        yield return Utill.GetDelay(skillData.Delay);
        playerFSM.ChangeState(EPlayerState.Idle);
    }
}
