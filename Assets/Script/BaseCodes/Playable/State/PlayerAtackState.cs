using UnityEngine;

public class PlayerAtackState : PlayerStateBase
{

    private int combo = 0;

    public PlayerAtackState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates){}

    public override void OnStateEnter()
    {
        controller.ConvertibleStates = convertibleStates;
        controller.Animator.SetTrigger("Atack");
    }

    public override void OnStateExit()
    {
        combo = 0;
    }

    public override void OnStateUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    controller.Animator.SetInteger("Combo", ++combo);
        //}
    }
}
