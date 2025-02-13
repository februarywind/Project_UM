using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator, EPlayerState[] convertibleStates) : base(controller, characterStat, playerFSM, animator, convertibleStates)
    {
    }

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(0);
        controller.ConvertibleStates = convertibleStates;
    }

    public override void OnStateExit()
    {
        Debug.Log("IdleExit");
    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(0);
    }
}
