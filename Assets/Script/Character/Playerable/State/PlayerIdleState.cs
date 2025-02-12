using UnityEngine;
using UnityEngine.Windows;

public class PlayerIdleState : PlayerStateBase
{
    public PlayerIdleState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM) : base(controller, characterStat, playerFSM){}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(0);
    }

    public override void OnStateExit()
    {
        Debug.Log("IdleExit");
    }

    public override void OnStateUpdate()
    {
        controller.DashHandler();
        controller.PlayerMove(0);
        if (controller.IsInput)
            playerFSM.ChangeState(EPlayerState.Walk);
    }
}
