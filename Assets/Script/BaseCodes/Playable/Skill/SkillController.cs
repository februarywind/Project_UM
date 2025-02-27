using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [SerializeField] Button ultimateSkillButton;
    [SerializeField] Button battleSkillButton;
    [SerializeField] Button attackButton;
    [SerializeField] Button dashButton;
    [SerializeField] Button jumpButton;
    [SerializeField] Button changeButton;

    private PlayableCharacterChange playable;

    private int combo;
    private void Awake()
    {
        playable = GetComponent<PlayableCharacterChange>();
        ultimateSkillButton.onClick.AddListener(UltimateSkill);
        battleSkillButton.onClick.AddListener(BattleSkill);
        attackButton.onClick.AddListener(AttackSkill);
        dashButton.onClick.AddListener(DashSkill);
        jumpButton.onClick.AddListener(JumpSkill);
        changeButton.onClick.AddListener(ChangeSkill);
    }
    /// <summary>
    /// 코루틴 대신 실행해드립니다.
    /// </summary>
    /// <param name="enumerator"></param>
    public void CoroutineAgent(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    // 상태변경 버튼 메서드들
    private void UltimateSkill()
    {
        if (playable.CurController.ConvertibleStates.Contains(EPlayerState.UltimateSkill))
            playable.CurController.PlayerFSM.ChangeState(EPlayerState.UltimateSkill);
    }
    private void BattleSkill()
    {
        if (playable.CurController.ConvertibleStates.Contains(EPlayerState.BattleSkill))
            playable.CurController.PlayerFSM.ChangeState(EPlayerState.BattleSkill);
    }
    private void AttackSkill()
    {
        if (playable.CurController.PlayerFSM.curEState == EPlayerState.Atack)
        {
            playable.CurController.Animator.SetInteger("Combo", ++combo);
        }
        else
        {
            combo = 0;
            playable.CurController.AtackHandler(true);
        }
    }
    private void DashSkill()
    {
        playable.CurController.DashHandler(true);
    }
    private void JumpSkill()
    {
        playable.CurController.JumpHandler(true);
    }
    private void ChangeSkill()
    {
        playable.CurController.ChangeHandler(true);
    }
}
