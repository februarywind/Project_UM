using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    public PlayerWalkState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates){}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(1);
        controller.ConvertibleStates = convertibleStates;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(controller.CharacterStat.WalkSpeed);
    }
}
