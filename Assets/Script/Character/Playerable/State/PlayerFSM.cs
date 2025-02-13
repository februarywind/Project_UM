using UnityEngine;

public enum EPlayerState
{
    Idle, Walk, Run, Jump, Dash, Atack, BattleSkill, UltimateSkill, Stop, Size
}

public class PlayerFSM
{
    private PlayerStateBase curState;
    private EPlayerState curEState = EPlayerState.Stop;
    public EPlayerState BeforeState;
    private PlayerStateBase[] playerStates = new PlayerStateBase[(int)EPlayerState.Size];
    public PlayerFSM(PlayerController controller, PlayerCharacterStat characterStat, Animator animator, BattleSkillBase battleSkill)
    {
        playerStates[(int)EPlayerState.Idle] = new PlayerIdleState(controller, new EPlayerState[] { EPlayerState.Walk, EPlayerState.Dash, EPlayerState.Atack, EPlayerState.Jump, EPlayerState.BattleSkill });
        playerStates[(int)EPlayerState.Walk] = new PlayerWalkState(controller, new EPlayerState[] { EPlayerState.Idle, EPlayerState.Dash, EPlayerState.Atack, EPlayerState.Jump, EPlayerState.BattleSkill });
        playerStates[(int)EPlayerState.Run] = new PlayerRunState(controller, new EPlayerState[] { EPlayerState.Idle, EPlayerState.Dash, EPlayerState.Atack, EPlayerState.Jump, EPlayerState.BattleSkill });
        playerStates[(int)EPlayerState.Dash] = new PlayerDashState(controller, new EPlayerState[] { EPlayerState.Stop });
        playerStates[(int)EPlayerState.Atack] = new PlayerAtackState(controller, new EPlayerState[] { EPlayerState.Dash, EPlayerState.Jump });
        playerStates[(int)EPlayerState.Jump] = new PlayerJumpState(controller, new EPlayerState[] { EPlayerState.Stop });
        playerStates[(int)EPlayerState.BattleSkill] = new PlayerBattleSkillState(controller, battleSkill, new EPlayerState[] { EPlayerState.Stop });

        ChangeState(EPlayerState.Idle);
    }
    public void ChangeState(EPlayerState state)
    {
        if (curEState == state)
            return;

        curState?.OnStateExit();

        BeforeState = curEState;
        curState = playerStates[(int)state];
        curEState = state;

        curState.OnStateEnter();
    }

    public void OnUpdate()
    {
        curState.OnStateUpdate();
    }
}
