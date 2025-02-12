using UnityEngine;

public class PlayerDashState : PlayerStateBase
{
    public PlayerDashState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM) : base(controller, characterStat, playerFSM){}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(5);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
