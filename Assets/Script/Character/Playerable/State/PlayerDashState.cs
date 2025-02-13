using UnityEngine;

public class PlayerDashState : PlayerStateBase
{
    public PlayerDashState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator, EPlayerState[] convertibleStates) : base(controller, characterStat, playerFSM, animator, convertibleStates) {}

    public override void OnStateEnter()
    {
        controller.ConvertibleStates = convertibleStates;
        animator.Play("Blend Tree", 0);
        controller.MoveAnimationPlay(5);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
