using System.Collections;
using UnityEngine;

public class U_UltimateSkill : UltimateSkillBase
{
    [SerializeField] GameObject[] invisibles;
    private RaycastHit[] hits;

    private void Awake()
    {
        hits = new RaycastHit[skillData.MaxTarget];
    }

    public override void UltimateSkillActivate()
    {
        StartCoroutine(U_Ultimate());
    }

    IEnumerator U_Ultimate()
    {
        // 무적
        stat.IsInvincibility = true;

        // 캐릭터 모델링 비활성화
        foreach (var item in invisibles)
            item.SetActive(false);

        // 반환되는 int 값은 덮어써진 값의 개수
        int indexLength = Physics.SphereCastNonAlloc(transform.position, skillData.Radius, Vector3.up, hits, 0, skillData.TargetLayer);
        for (int i = 0; i < skillData.HitCount; i++)
        {
            for (int j = 0; j < indexLength; j++)
            {
                hits[j].transform.GetComponent<IDamagable>().TakeDamage(stat.AttackPower * skillData.DamageRatio, EAtackElement.Electric, transform);
            }
            EffectManager.instance.ParticlePlay("Slash", 1f, transform.position + new Vector3(Random.Range(-1.5f, 1.5f), 1, Random.Range(-1.5f, 1.5f)), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            yield return Utill.GetDelay(0.1f);
        }

        foreach (var item in invisibles)
            item.SetActive(true);

        stat.IsInvincibility = false;
        playerFSM.ChangeState(EPlayerState.Idle);
        CoolDownStart();
    }
}
