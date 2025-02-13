using UnityEngine;

public class PlayerBattleSkillState : PlayerStateBase
{
    private BattleSkillBase battleSkill;
    public PlayerBattleSkillState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator, EPlayerState[] convertibleStates, BattleSkillBase battleSkill) : base(controller, characterStat, playerFSM, animator, convertibleStates)
    {
        this.battleSkill = battleSkill;
    }

    public override void OnStateEnter()
    {
        controller.ConvertibleStates = convertibleStates;
        battleSkill.BattleSkillActivate();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
