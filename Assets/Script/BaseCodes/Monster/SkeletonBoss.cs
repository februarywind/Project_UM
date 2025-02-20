using UnityEngine;

public class SkeletonBoss : MonsterBase
{
    [SerializeField] GameObject potal;
    protected override void Dead()
    {
        base.Dead();
        potal.SetActive(true);
    }
}
