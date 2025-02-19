public class PlayerBattleSkillState : PlayerStateBase
{
    private BattleSkillBase battleSkill;

    public PlayerBattleSkillState(PlayerController controller, BattleSkillBase battleSkill, EPlayerState[] convertibleStates) : base(controller, convertibleStates)
    {
        this.battleSkill = battleSkill;
    }

    public override void OnStateEnter()
    {
        if (battleSkill.IsCoolTime)
        {
            controller.PlayerFSM.ChangeState(controller.PlayerFSM.BeforeState);
            return;
        }
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
