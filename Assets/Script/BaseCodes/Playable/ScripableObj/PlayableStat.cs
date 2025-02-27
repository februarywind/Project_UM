using System;
using UnityEngine;

public enum EStat
{
    MaxHp, MaxStamina, AttackPower, Size
}

[CreateAssetMenu(fileName = "PlayerBattleStat", menuName = "Scriptable Objects/PlayerBattleStat")]
public class PlayableStat : ScriptableObject
{
    [SerializeField] int level;
    public int Level => level;

    public int StatPoint;

    [SerializeField] float[] statUpValue = new float[(int)EStat.Size];

    [Header("추가 능력치")]
    [SerializeField] float[] levelUpStats = new float[(int)EStat.Size];

    [Header("최대 능력치")]

    [SerializeField] float maxHp;
    public float MaxHp { get => maxHp + levelUpStats[(int)EStat.MaxHp]; }
    [SerializeField] float maxStamina;
    public float MaxStamina { get => maxStamina + levelUpStats[(int)EStat.MaxStamina]; }

    [SerializeField] float attackPower;
    public float AttackPower { get => attackPower + levelUpStats[(int)EStat.AttackPower]; }

    [Header("실시간 능력치")]

    [SerializeField] float curHp;
    public float CurHp
    {
        get => curHp;
        set
        {
            curHp = Mathf.Clamp(value, 0, MaxHp);
            OnChangeCurHp?.Invoke(CurHp);
        }
    }


    [SerializeField] float curStamina;
    public float CurStamina
    {
        get => curStamina;
        set
        {
            curStamina = Mathf.Clamp(value, 0, MaxStamina);
            OnChangeCurStamina?.Invoke(CurStamina);
        }
    }
    [Header("상태이상")]
    public bool IsInvincibility;

    [Header("스테미나")]
    [SerializeField] float staminaRegenWaitTime;
    public float StaminaRegenWaitTime => staminaRegenWaitTime;

    [SerializeField] float staminaRFS;
    public float StaminaRFS => staminaRFS;

    [SerializeField] float dashStaminaValue;
    public float DashStaminaValue => dashStaminaValue;

    [SerializeField] float runStaminaVFS;
    public float RunStaminaVFS => runStaminaVFS;

    public event Action<int> OnChangeLevel;
    public event Action<int> OnChangeStatPoint;
    public event Action<float> OnChangeCurHp;
    public event Action<float> OnChangeCurStamina;
    public event Action<float> OnChangeAttackPower;
    public event Action<float[]> OnChangeFixedStat;

    private void AllChange()
    {
        OnChangeCurHp?.Invoke(CurHp);
        OnChangeCurStamina?.Invoke(CurStamina);
        OnChangeAttackPower?.Invoke(AttackPower);
        OnChangeFixedStat?.Invoke(levelUpStats);
    }
    public void RemoveAllEvent()
    {
        OnChangeLevel = null;
        OnChangeCurHp = null;
        OnChangeCurStamina = null;
        OnChangeAttackPower = null;
        OnChangeFixedStat = null;
    }

    public void LevelUp()
    {
        level++;
        StatPoint++;
        OnChangeLevel?.Invoke(level);
    }
    public void StatUp(EStat stat)
    {
        if (StatPoint < 1)
        {
            return;
        }
        OnChangeStatPoint?.Invoke(--StatPoint);
        levelUpStats[(int)stat] += statUpValue[(int)stat];
        AllChange();
    }
}
