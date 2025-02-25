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
        playerController.Animator.SetTrigger("Heal");
        float healValue = stat.AttackPower * skillData.DamageRatio;
        yield return Util.GetDelay(skillData.Delay);
        EffectManager.instance.ParticlePlay("Healing", 2, transform.position, Quaternion.identity, transform);
        int indexLength = Physics.SphereCastNonAlloc(transform.position, skillData.Radius, Vector3.up, hits, skillData.Range, skillData.TargetLayer);
        for (int i = 0; i < indexLength; i++)
        {
            hits[i].transform.GetComponent<PlayerController>().StatController.Stat.CurHp += healValue;
            DamagePopUpManager.instance.ShowDamagePopUp(hits[i].transform.position, $"{healValue}", Color.green);
        }
        CoolDownStart();
        playerFSM.ChangeState(EPlayerState.Idle);
    }
}
