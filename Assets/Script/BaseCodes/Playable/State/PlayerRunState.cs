using UnityEngine;

public class PlayerRunState : PlayerStateBase
{
    public PlayerRunState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates){}

    public override void OnStateEnter()
    {
        controller.MoveAnimationPlay(3);
        controller.ConvertibleStates = convertibleStates;
    }

    public override void OnStateExit()
    {
        controller.StatController.StaminaRegen();
    }

    public override void OnStateUpdate()
    {
        controller.PlayerMove(controller.CharacterMovingStat.RunSpeed);
        controller.StatController.Stat.CurStamina -= controller.StatController.Stat.RunStaminaVFS * Time.deltaTime;
        if (controller.StatController.Stat.CurStamina < 1)
        {
            controller.PlayerFSM.ChangeState(EPlayerState.Idle);
        }
    }
}
