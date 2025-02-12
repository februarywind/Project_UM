using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    public PlayerWalkState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM) : base(controller, characterStat, playerFSM) { }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("walkexit");
    }

    public override void OnStateUpdate()
    {
        controller.MoveAnimationPlay(1);
        controller.DashHandler();
        controller.PlayerMove(characterStat.WalkSpeed);
        if (!controller.IsInput)
            playerFSM.ChangeState(EPlayerState.Idle);
    }
}
