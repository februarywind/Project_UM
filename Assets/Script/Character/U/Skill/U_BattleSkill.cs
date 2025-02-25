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
        playerController.Animator.SetTrigger("Battle");
        yield return Util.GetDelay(skillData.Delay);
        Vector3 skillDir = playerController.IsInput ? playerController.InputDir : transform.forward;
        EffectManager.instance.ParticlePlay("Flares", 1f, Vector3.up * 0.5f + transform.position + skillDir * skillData.Range / 2, Quaternion.LookRotation(skillDir) * Quaternion.Euler(0, 90, 0));
        int indexLength = Physics.SphereCastNonAlloc(transform.position, skillData.Radius, skillDir, hits, skillData.Range, skillData.TargetLayer);
        for (int i = 0; i < indexLength; i++)
        {
            hits[i].transform.GetComponent<IDamagable>().TakeDamage(stat.AttackPower * skillData.DamageRatio, EAtackElement.Electric, transform);
        }

        playerController.characterController.excludeLayers += skillData.TargetLayer;
        playerController.characterController.Move(skillDir * skillData.Range);
        playerController.characterController.excludeLayers -= skillData.TargetLayer;

        playerFSM.ChangeState(EPlayerState.Idle);

        CoolDownStart();
    }

    private void OnDrawGizmos()
    {
        // 시작점 원 (SphereCast의 초기 위치)
        Gizmos.DrawWireSphere(transform.position, skillData.Radius);

        // 끝점 원 (SphereCast가 도달할 위치)
        Gizmos.DrawWireSphere(transform.position + transform.forward * skillData.Range, skillData.Radius);
    }
}
