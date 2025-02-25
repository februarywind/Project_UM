using System.Collections;
using UnityEngine;

public class M_UltimateSkill : UltimateSkillBase
{
    private RaycastHit[] hits;

    private SkillCamera skillCamera;

    private void Awake()
    {
        hits = new RaycastHit[skillData.MaxTarget];
        skillCamera = GetComponent<SkillCamera>();
    }
    public override void UltimateSkillActivate()
    {
        StartCoroutine(HealSkill());
    }

    IEnumerator HealSkill()
    {
        stat.IsInvincibility = true;
        skillCamera.UtimateViewCamera(true);
        playerController.Animator.SetTrigger("Ultimate");
        yield return Util.GetDelay(skillData.Delay);
        EffectManager.instance.ParticlePlay("Healing circle", skillData.HitCount * 0.5f, transform.position, Quaternion.identity);

        // 캐릭터 비 활성화 때도 동작하도록 외부에서 코루틴을 실행함
        skillController.CoroutineAgent(UltimateSkill(transform.position));

        stat.IsInvincibility = false;
        CoolDownStart();
        playerFSM.ChangeState(EPlayerState.Idle);

        skillCamera.UtimateViewCamera(false);
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
                    hits[j].transform.GetComponent<IDamagable>().TakeDamage(stat.AttackPower * skillData.DamageRatio, EAtackElement.Normal, transform);
                }
                else
                {
                    hits[j].transform.GetComponent<PlayerController>().StatController.Stat.CurHp += stat.AttackPower * skillData.DamageRatio;
                    DamagePopUpManager.instance.ShowDamagePopUp(hits[j].transform.position, $"{stat.AttackPower * skillData.DamageRatio:F0}", Color.green);
                }
            }
            yield return Util.GetDelay(0.5f);
        }
    }
}
