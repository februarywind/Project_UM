using UnityEngine;

public class PlayerUltimateSkillState : PlayerStateBase
{
    private UltimateSkillBase ultimateSkill;
    public PlayerUltimateSkillState(PlayerController controller, UltimateSkillBase ultimateSkill, EPlayerState[] convertibleStates) : base(controller, convertibleStates) 
    {
        this.ultimateSkill = ultimateSkill;
    }

    public override void OnStateEnter()
    {
        if (ultimateSkill.IsCoolTime)
        {
            controller.PlayerFSM.ChangeState(controller.PlayerFSM.BeforeState);
            return;
        }
        controller.ConvertibleStates = convertibleStates;
        ultimateSkill.UltimateSkillActivate();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
    }
}
