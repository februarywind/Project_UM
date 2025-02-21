using UnityEngine;

public class RangeAtackBase : MonoBehaviour
{
    [SerializeField] RangeAtackData rangeAtackData;
    private PlayableStat stat;
    private void Awake()
    {
        stat = GetComponent<PlayableStatController>().Stat;
    }

    // 애니메이션 이벤트
    private void RangeAtack(int index)
    {
        Attack(rangeAtackData.RangeDatas[index]);
    }
    private void Attack(RangeData rangeData)
    {
        if (Physics.SphereCast(transform.position, rangeData.Radius, transform.forward, out RaycastHit hit, rangeData.Range, rangeAtackData.TargetLayer))
        {
            hit.transform.GetComponent<IDamagable>().TakeDamage(stat.AttackPower * rangeData.DamageRatio, EAtackElement.Normal, transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // 시작점 원 (SphereCast의 초기 위치)
        Gizmos.DrawWireSphere(transform.position, rangeAtackData.RangeDatas[rangeAtackData.DrawCombo].Radius);

        // 끝점 원 (SphereCast가 도달할 위치)
        Gizmos.DrawWireSphere(transform.position + transform.forward * rangeAtackData.RangeDatas[rangeAtackData.DrawCombo].Range, rangeAtackData.RangeDatas[rangeAtackData.DrawCombo].Radius);
    }
}
