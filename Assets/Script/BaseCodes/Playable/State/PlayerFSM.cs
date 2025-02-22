using UnityEngine;

public enum EPlayerState
{
    Idle, Walk, Run, Jump, Dash, Atack, BattleSkill, UltimateSkill, Stop, Change, Size
}

public class PlayerFSM
{
    private PlayerStateBase curState;
    public EPlayerState curEState { get; private set; } = EPlayerState.Stop;
    public EPlayerState BeforeState;
    private PlayerStateBase[] playerStates = new PlayerStateBase[(int)EPlayerState.Size];
    public PlayerFSM(PlayerController controller, PlayerMovingStat characterStat, Animator animator, BattleSkillBase battleSkill, UltimateSkillBase ultimateSkill)
    {
        playerStates[(int)EPlayerState.Idle] = new PlayerIdleState(controller, new EPlayerState[] { EPlayerState.Walk, EPlayerState.Dash, EPlayerState.Atack, EPlayerState.Jump, EPlayerState.BattleSkill, EPlayerState.UltimateSkill, EPlayerState.Change });
        playerStates[(int)EPlayerState.Walk] = new PlayerWalkState(controller, new EPlayerState[] { EPlayerState.Idle, EPlayerState.Dash, EPlayerState.Atack, EPlayerState.Jump, EPlayerState.BattleSkill, EPlayerState.UltimateSkill, EPlayerState.Change });
        playerStates[(int)EPlayerState.Run] = new PlayerRunState(controller, new EPlayerState[] { EPlayerState.Idle, EPlayerState.Dash, EPlayerState.Atack, EPlayerState.Jump, EPlayerState.BattleSkill, EPlayerState.UltimateSkill, EPlayerState.Change });
        playerStates[(int)EPlayerState.Dash] = new PlayerDashState(controller, new EPlayerState[] { EPlayerState.Stop });
        playerStates[(int)EPlayerState.Atack] = new PlayerAtackState(controller, new EPlayerState[] { EPlayerState.Dash, EPlayerState.Jump });
        playerStates[(int)EPlayerState.Jump] = new PlayerJumpState(controller, new EPlayerState[] { EPlayerState.Stop });
        playerStates[(int)EPlayerState.BattleSkill] = new PlayerBattleSkillState(controller, battleSkill, new EPlayerState[] { EPlayerState.Stop });
        playerStates[(int)EPlayerState.UltimateSkill] = new PlayerUltimateSkillState(controller, ultimateSkill, new EPlayerState[] { EPlayerState.Stop });
        playerStates[(int)EPlayerState.Change] = new PlayerChangePlayableState(controller, new EPlayerState[] { EPlayerState.Stop });

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
