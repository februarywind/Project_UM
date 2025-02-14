using UnityEngine;

public class RangeAtackBase : MonoBehaviour
{
    [SerializeField] RangeAtackData rangeAtackData;

    // 애니메이션 이벤트
    private void RangeAtack(int index)
    {
        Attack(rangeAtackData.RangeDatas[index]);
    }
    private void Attack(RangeData rangeData)
    {
        if (Physics.SphereCast(transform.position, rangeData.Radius, transform.forward, out RaycastHit hit, rangeData.Range, rangeAtackData.TargetLayer))
        {
            hit.transform.GetComponent<IDamagable>().TakeDamage(rangeData.Damage, EAtackElement.Normal);
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
