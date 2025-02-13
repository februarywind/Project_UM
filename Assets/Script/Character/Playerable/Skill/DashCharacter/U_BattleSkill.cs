using System.Collections;
using UnityEngine;

public class U_BattleSkill : BattleSkillBase
{
    [SerializeField] float dagame;
    [SerializeField] float range;
    [SerializeField] float delay;
    [SerializeField] LayerMask excludeLayer;

    public override void BattleSkillActivate()
    {
        StartCoroutine(DashSkill());
    }

    IEnumerator DashSkill()
    {
        Vector3 skillDir = playerController.IsInput ? playerController.InputDir : transform.forward;

        if (Physics.SphereCast(transform.position, 1, skillDir, out RaycastHit hit, range, excludeLayer))
        {
            hit.transform.GetComponent<IDamagable>().TakeDamage(dagame);
        }

        playerController.characterController.excludeLayers += excludeLayer;
        playerController.characterController.Move(skillDir * range);
        playerController.characterController.excludeLayers -= excludeLayer;
        yield return Utill.GetDelay(delay);
        playerFSM.ChangeState(EPlayerState.Idle);

        SkillCoolTime();
    }

    //private void OnDrawGizmos()
    //{
    //    // 시작점 원 (SphereCast의 초기 위치)
    //    Gizmos.DrawWireSphere(transform.position, 1);

    //    // 끝점 원 (SphereCast가 도달할 위치)
    //    Gizmos.DrawWireSphere(transform.position + transform.forward * range, 1);
    //}
}
