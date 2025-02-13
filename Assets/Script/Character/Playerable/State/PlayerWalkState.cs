using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    public PlayerWalkState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator, EPlayerState[] convertibleStates) : base(controller, characterStat, playerFSM, animator, convertibleStates)
    {
    }

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
        controller.PlayerMove(characterStat.WalkSpeed);
    }
}
