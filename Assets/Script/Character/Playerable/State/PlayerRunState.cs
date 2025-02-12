using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public PlayerRunState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator) : base(controller, characterStat, playerFSM, animator) {}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(3);
        controller.ConvertibleStates = new EPlayerState[2] { EPlayerState.Idle, EPlayerState.Dash };
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(characterStat.RunSpeed);
    }
}
