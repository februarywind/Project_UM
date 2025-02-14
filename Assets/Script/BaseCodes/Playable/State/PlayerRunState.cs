using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public PlayerRunState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates){}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(3);
        controller.ConvertibleStates = convertibleStates;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(controller.CharacterMovingStat.RunSpeed);
    }
}
