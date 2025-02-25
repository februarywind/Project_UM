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
        // 플레이중인 애니메이션이 MoveBlend와 다르다면 리턴, 더 좋은 방식이 있을까?
        if (!controller.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
        {
            return;
        }
        controller.PlayerMove(controller.CharacterMovingStat.WalkSpeed);
    }
}
