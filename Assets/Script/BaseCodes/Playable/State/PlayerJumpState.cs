public class PlayerJumpState : PlayerStateBase
{
    public PlayerJumpState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates) { }
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
        controller.PlayerMove(controller.PlayerFSM.BeforeState == EPlayerState.Run ? controller.CharacterMovingStat.RunSpeed : controller.CharacterMovingStat.WalkSpeed);

        if (!isAir)
            isAir = !controller.IsGround();
        if (isAir && controller.IsGround())
            controller.PlayerFSM.ChangeState(controller.PlayerFSM.BeforeState == EPlayerState.Run ? EPlayerState.Run : EPlayerState.Idle);
    }
}
