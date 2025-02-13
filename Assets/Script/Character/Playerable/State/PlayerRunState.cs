using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public PlayerRunState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator, EPlayerState[] convertibleStates) : base(controller, characterStat, playerFSM, animator, convertibleStates) {}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(3);
        controller.ConvertibleStates = convertibleStates;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(characterStat.RunSpeed);
    }
}
