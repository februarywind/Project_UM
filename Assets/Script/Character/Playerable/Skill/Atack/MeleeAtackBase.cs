using UnityEngine;

public class MeleeAtackBase : MonoBehaviour
{
    [SerializeField] MeleeAtackData meleeAtackData;

    // 애니메이션 이벤트
    private void MeleeAtack(int index)
    {
        Attack(meleeAtackData.MeleeDatas[index]);
    }
    private void Attack(MeleeData meleeData)
    {
        // 전방 앞에 있는 몬스터들을 확인하고 피격을 진행해 준다.

        // 1. 범위 안에 몬스터들을 확인
        // NonAlloc 버전을 사용하면 최적화면에서 도움될 수 있음
        Collider[] colliders = Physics.OverlapSphere(transform.position, meleeData.Range, meleeAtackData.TargetLayer);
        foreach (Collider col in colliders)
        {
            // 2. 탐지된 몬스터가 판정을 위한 각도 내에 있는지 확인해야 한다.
            // y의 경우 연산에 포함하지 않는게 좋다.
            Vector3 source = transform.position.RemoveOne(RemoveDir.Y);
            Vector3 dest = col.transform.position.RemoveOne(RemoveDir.Y);

            Vector3 targetDir = (dest - source).normalized;

            // 플레이어 정면으로부터 몬스터의 방향으로 향하는 각도를 구한다.
            float targetAngle = Vector3.Angle(transform.forward, targetDir);
            // 좌측 angle, 우측 angle에 대해 반절씩 확인해야 하므로
            // 0.5를 곱해 angle의 절반만큼의 값과 비교한다.
            if (targetAngle > meleeData.Angle * 0.5f)
                continue;
            col.GetComponent<IDamagable>().TakeDamage(meleeData.Damage, EAtackElement.Normal);
        }
    }

    private void OnDrawGizmos()
    {
        // 거리 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAtackData.MeleeDatas[meleeAtackData.DrawCombo].Range);

        // 각도 그리기
        Vector3 rightDir = Quaternion.Euler(0, meleeAtackData.MeleeDatas[meleeAtackData.DrawCombo].Angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, meleeAtackData.MeleeDatas[meleeAtackData.DrawCombo].Angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * meleeAtackData.MeleeDatas[meleeAtackData.DrawCombo].Range);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * meleeAtackData.MeleeDatas[meleeAtackData.DrawCombo].Range);
    }
}
