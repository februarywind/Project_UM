using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] UltimateSkillBase ultimateSkill;
    [SerializeField] BattleSkillBase battleSkill;
    public void UtimateSkillCoolTimeStart()
    {
        StartCoroutine(ultimateSkill.CoolDown());
    }
    public void BattleSkillCoolTimeStart()
    {
        StartCoroutine(battleSkill.CoolDown());
    }
}
