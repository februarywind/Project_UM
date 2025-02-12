using UnityEngine;

public class PlayerAtackState : PlayerStateBase
{
    public PlayerAtackState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator) : base(controller, characterStat, playerFSM, animator) {}

    public override void OnStateEnter()
    {
        animator.SetTrigger("Atack");
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
