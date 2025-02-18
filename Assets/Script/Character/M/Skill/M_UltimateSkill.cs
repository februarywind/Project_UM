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
        EffectManager.instance.ParticlePlay("Healing circle", skillData.HitCount * 0.5f, transform.position, Quaternion.identity);
        skillController.CoroutineAgent(UltimateSkill(transform.position));
        CoolDownStart();
        playerFSM.ChangeState(EPlayerState.Idle);
    }

    public IEnumerator UltimateSkill(Vector3 pos)
    {
        for (int i = 0; i < skillData.HitCount; i++)
        {
            int indexLength = Physics.SphereCastNonAlloc(pos, skillData.Radius, Vector3.up, hits, skillData.Range, skillData.TargetLayer);
            for (int j = 0; j < indexLength; j++)
            {

                if (1 << hits[j].transform.gameObject.layer == LayerMask.GetMask("Monster"))
                {
                    hits[j].transform.GetComponent<IDamagable>().TakeDamage(stat.AttackPower * skillData.DamageRatio, EAtackElement.Normal);
                }
                else
                {
                    hits[j].transform.GetComponent<PlayerController>().StatController.Stat.CurHp += stat.AttackPower * skillData.DamageRatio;
                    DamagePopUpManager.instance.ShowDamagePopUp(hits[j].transform.position, $"{stat.AttackPower * skillData.DamageRatio}", Color.green);
                }
            }
            yield return Utill.GetDelay(0.5f);
        }
    }
}
