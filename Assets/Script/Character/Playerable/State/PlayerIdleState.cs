using UnityEngine;
using UnityEngine.Windows;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator) : base(controller, characterStat, playerFSM, animator)
    {
    }

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(0);
        controller.ConvertibleStates = new EPlayerState[2] {EPlayerState.Walk, EPlayerState.Dash};
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
