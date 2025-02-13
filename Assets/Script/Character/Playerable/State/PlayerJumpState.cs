using System.Collections;
using UnityEngine;

public class PlayerJumpState : PlayerStateBase
{
    public PlayerJumpState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator, EPlayerState[] convertibleStates) : base(controller, characterStat, playerFSM, animator, convertibleStates){}
    private bool isAir;
    public override void OnStateEnter()
    {
        isAir = false;
        controller.ConvertibleStates = convertibleStates;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(playerFSM.BeforeState == EPlayerState.Run ? characterStat.RunSpeed : characterStat.WalkSpeed);

        if (!isAir) 
            isAir = !controller.IsGround();
        if (isAir && controller.IsGround())
            playerFSM.ChangeState(playerFSM.BeforeState == EPlayerState.Run ? EPlayerState.Run : EPlayerState.Idle);
    }
}
