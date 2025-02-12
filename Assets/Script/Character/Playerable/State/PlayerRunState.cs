using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public PlayerRunState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM) : base(controller, characterStat, playerFSM){}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(3);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        controller.DashHandler();
        controller.PlayerMove(characterStat.RunSpeed);
        if (!controller.IsInput)
            playerFSM.ChangeState(EPlayerState.Idle);
    }
}
