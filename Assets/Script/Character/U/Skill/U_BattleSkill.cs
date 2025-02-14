using System;
using System.Collections;
using UnityEngine;

public class U_BattleSkill : BattleSkillBase
{
    [SerializeField] float damage;
    [SerializeField] float radius;
    [SerializeField] float range;
    [SerializeField] float delay;
    [SerializeField] LayerMask excludeLayer;
    [SerializeField] int maxTarget;
    private RaycastHit[] hits;

    private void Awake()
    {
        hits = new RaycastHit[maxTarget];
    }

    public override void BattleSkillActivate()
    {
        StartCoroutine(DashSkill());
    }

    IEnumerator DashSkill()
    {
        Array.Fill(hits, new RaycastHit());
        Vector3 skillDir = playerController.IsInput ? playerController.InputDir : transform.forward;
        Physics.SphereCastNonAlloc(transform.position, radius, skillDir, hits, range, excludeLayer);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider == null) continue;
            hit.transform.GetComponent<IDamagable>().TakeDamage(damage, EAtackElement.Electric);
        }

        playerController.characterController.excludeLayers += excludeLayer;
        playerController.characterController.Move(skillDir * range);
        playerController.characterController.excludeLayers -= excludeLayer;

        yield return Utill.GetDelay(delay);

        playerFSM.ChangeState(EPlayerState.Idle);

        SkillCoolTime();
    }

    private void OnDrawGizmos()
    {
        // 시작점 원 (SphereCast의 초기 위치)
        Gizmos.DrawWireSphere(transform.position, radius);

        // 끝점 원 (SphereCast가 도달할 위치)
        Gizmos.DrawWireSphere(transform.position + transform.forward * range, radius);
    }
}
