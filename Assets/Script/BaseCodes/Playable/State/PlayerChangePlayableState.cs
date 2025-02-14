using UnityEngine;

public class PlayerChangePlayableState : PlayerStateBase
{
    public PlayerChangePlayableState(PlayerController controller, EPlayerState[] convertibleStates) : base(controller, convertibleStates){}

    public override void OnStateEnter()
    {
        controller.CharacterChange.ChangeCharacter(controller);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
