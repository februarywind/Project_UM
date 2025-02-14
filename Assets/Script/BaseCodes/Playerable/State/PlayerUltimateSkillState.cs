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
        Debug.Log(1111111);
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
