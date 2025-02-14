using System;
using System.Collections;
using UnityEngine;

public class U_UltimateSkill : UltimateSkillBase
{
    [SerializeField] int hitCount;
    [SerializeField] float damage;
    [SerializeField] float radius;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] int maxTarget;

    private RaycastHit[] hits;

    private void Awake()
    {
        hits = new RaycastHit[maxTarget];
    }

    public override void UltimateSkillActivate()
    {
        StartCoroutine(U_Ultimate());
    }

    IEnumerator U_Ultimate()
    {
        Array.Fill(hits, new RaycastHit());
        Physics.SphereCastNonAlloc(transform.position, radius, Vector3.up, hits, 1, targetLayer);
        for (int i = 0; i < hitCount; i++)
        {
            foreach (var item in hits)
            {
                if (item.collider == null) continue;
                item.transform.GetComponent<IDamagable>().TakeDamage(damage, EAtackElement.Electric);
            }
            yield return Utill.GetDelay(0.1f);
        }
        playerFSM.ChangeState(EPlayerState.Idle);
        SkillCoolTime();
    }
}
