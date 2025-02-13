using UnityEngine;

public class PlayerAtackState : PlayerStateBase
{
    public PlayerAtackState(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator, EPlayerState[] convertibleStates) : base(controller, characterStat, playerFSM, animator, convertibleStates){}

    private int combo = 0;
    public override void OnStateEnter()
    {
        controller.ConvertibleStates = convertibleStates;
        animator.SetTrigger("Atack");
    }

    public override void OnStateExit()
    {
        combo = 0;
    }

    public override void OnStateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("Combo", ++combo);
        }
    }
}
