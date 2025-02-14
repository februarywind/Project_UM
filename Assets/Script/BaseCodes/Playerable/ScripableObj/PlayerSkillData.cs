using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillData", menuName = "Scriptable Objects/PlayerSkillData")]
public class PlayerSkillData : ScriptableObject
{
    [SerializeField] float coolTime;
    public float CoolTime => coolTime;

    [SerializeField] float damage;
    public float Damage => damage;

    [SerializeField] float radius;
    public float Radius => radius;

    [SerializeField] float range;
    public float Range => range;

    [SerializeField] float delay;
    public float Delay => delay;

    [SerializeField] int maxTarget;
    public int MaxTarget => maxTarget;

    [SerializeField] int hitCount;
    public int HitCount => hitCount;

    [SerializeField] LayerMask targetLayer;
    public int TargetLayer => targetLayer;
}
