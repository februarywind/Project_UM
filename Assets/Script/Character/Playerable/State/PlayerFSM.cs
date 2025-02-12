using UnityEngine;

public enum EPlayerState
{
    Idle, Walk, Run, Jump, Dash, Atack, BattleSkill, UltimateSkill, Size
}

public class PlayerFSM
{
    public PlayerStateBase CurState;
    private EPlayerState curEState = EPlayerState.Size;
    private PlayerStateBase[] playerStates = new PlayerStateBase[(int)EPlayerState.Size];
    public PlayerFSM(PlayerController controller, PlayerCharacterStat characterStat, Animator animator)
    {
        playerStates[(int)EPlayerState.Idle] = new PlayerIdleState(controller, characterStat, this, animator);
        playerStates[(int)EPlayerState.Walk] = new PlayerWalkState(controller, characterStat, this, animator);
        playerStates[(int)EPlayerState.Run] = new PlayerRunState(controller, characterStat, this, animator);
        playerStates[(int)EPlayerState.Dash] = new PlayerDashState(controller, characterStat, this, animator);

        ChangeState(EPlayerState.Idle);
    }
    public void ChangeState(EPlayerState state)
    {
        if (curEState == state)
            return;
        CurState?.OnStateExit();
        CurState = playerStates[(int)state];
        CurState.OnStateEnter();
        curEState = state;
    }

    public void OnUpdate()
    {
        CurState.OnStateUpdate();
    }
}
