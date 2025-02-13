public abstract class PlayerStateBase
{
    protected PlayerController controller;
    protected EPlayerState[] convertibleStates;
    protected PlayerStateBase(PlayerController controller, EPlayerState[] convertibleStates)
    {
        this.controller = controller;
        this.convertibleStates = convertibleStates;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
