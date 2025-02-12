using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    public PlayerWalkState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator) : base(controller, characterStat, playerFSM, animator)
    {
    }

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(1);
        controller.ConvertibleStates = new EPlayerState[2] { EPlayerState.Idle, EPlayerState.Dash };
    }

    public override void OnStateExit()
    {
        Debug.Log("walkexit");
    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(characterStat.WalkSpeed);
    }
}
