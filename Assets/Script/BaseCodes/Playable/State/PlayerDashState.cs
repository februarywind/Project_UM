using UnityEngine;

public class PlayerDashState : PlayerStateBase
{
    public PlayerDashState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates) {}

    public override void OnStateEnter()
    {
        controller.ConvertibleStates = convertibleStates;
        controller.Animator.Play("Blend Tree", 0);
        controller.MoveAnimationPlay(5);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
