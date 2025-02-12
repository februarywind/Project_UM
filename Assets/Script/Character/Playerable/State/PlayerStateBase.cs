using UnityEngine;

public abstract class PlayerStateBase
{
    protected PlayerController controller;
    protected PlayerCharacterStat characterStat;
    protected PlayerFSM playerFSM;
    protected Animator animator;
    protected PlayerStateBase(PlayerController controller, PlayerCharacterStat characterStat, PlayerFSM playerFSM, Animator animator)
    {
        this.controller = controller;
        this.characterStat = characterStat;
        this.playerFSM = playerFSM;
        this.animator = animator;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
