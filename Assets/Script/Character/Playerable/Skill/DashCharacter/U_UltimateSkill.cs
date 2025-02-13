using System.Collections;
using UnityEngine;

public class U_UltimateSkill : UltimateSkillBase
{
    [SerializeField] int hitCount;
    [SerializeField] float damage;
    [SerializeField] float radius;
    [SerializeField] LayerMask targetLayer;

    public override void UltimateSkillActivate()
    {
        StartCoroutine(U_Ultimate());
    }

    IEnumerator U_Ultimate()
    {
        for (int i = 0; i < hitCount; i++)
        {
            foreach (var item in Physics.SphereCastAll(transform.position, radius, Vector3.up, 3, targetLayer))
            {
                item.transform.GetComponent<IDamagable>().TakeDamage(damage);
            }
            yield return Utill.GetDelay(0.1f);
        }
        playerFSM.ChangeState(EPlayerState.Idle);
        SkillCoolTime();
    }
}
