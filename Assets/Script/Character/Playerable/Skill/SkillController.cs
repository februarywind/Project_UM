using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] BattleSkillBase battleSkill;
    [SerializeField] DefaultAtackBase defaultAtack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            battleSkill.BattleSkillActivate();
        }
        if (Input.GetMouseButtonDown(0))
        {
            defaultAtack.DefaultAtackActivate();
        }
    }
}
