using UnityEngine;

public class BattleSkillBase : MonoBehaviour
{
    public virtual void BattleSkillActivate()
    {
        Debug.Log("전투 스킬 사용");
    }
}
