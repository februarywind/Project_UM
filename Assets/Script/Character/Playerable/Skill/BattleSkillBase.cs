using UnityEngine;

public class BattleSkillBase : MonoBehaviour
{
    public bool IsActionUse { get; set; }
    public virtual void BattleSkillActivate()
    {
        Debug.Log("전투 스킬 사용");
    }
}
