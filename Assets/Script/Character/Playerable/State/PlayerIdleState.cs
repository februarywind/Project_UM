using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates){}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(0);
        controller.ConvertibleStates = convertibleStates;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(0);
    }
}
