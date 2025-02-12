using UnityEngine;

public class PlayerDashState : PlayerStateBase
{
    public PlayerDashState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator) : base(controller, characterStat, playerFSM, animator) {}

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
