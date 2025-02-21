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

    [Header("추가 능력치")]
    [SerializeField] float[] statPers = new float[(int)EStat.Size];
    [SerializeField] float[] fixedstats = new float[(int)EStat.Size];
    public float[] StatPers => statPers;
    public float[] FixedStats => fixedstats;

    [Header("최대 능력치")]

    [SerializeField] float maxHp;
    public float MaxHp
    {
        get
        {
            float baseHp = maxHp + fixedstats[(int)EStat.MaxHp];
            return baseHp * (1 + GetPerStat(EStat.MaxHp) * 0.01f);
        }
    }
    [SerializeField] float maxStamina;
    public float MaxStamina
    {
        get
        {
            float baseStamina = maxStamina + fixedstats[(int)EStat.MaxStamina];
            return baseStamina * (1 + GetPerStat(EStat.MaxStamina) * 0.01f);
        }
    }

    [SerializeField] float attackPower;
    public float AttackPower
    {
        get
        {
            float baseAttackPower = attackPower + fixedstats[(int)EStat.AttackPower];
            return baseAttackPower * (1 + GetPerStat(EStat.AttackPower) * 0.01f);
        }
    }

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



    public void SetPerStat(EStat statPer, float value)
    {
        statPers[(int)statPer] += value;
        AllChange();
    }
    public float GetPerStat(EStat statPer)
    {
        return statPers[(int)statPer];
    }

    public void SetFixedStat(EStat fixedStat, float value)
    {
        fixedstats[(int)fixedStat] += value; 
        AllChange();
    }
    public float GetFixedStat(EStat fixedStat)
    {
        return fixedstats[(int)fixedStat];
    }

    public event Action<int> OnChangeLevel;
    public event Action<float> OnChangeCurHp;
    public event Action<float> OnChangeCurStamina;
    public event Action<float> OnChangeAttackPower;
    public event Action<float[]> OnChangeFixedStat;
    public event Action<float[]> OnChangePerStat;

    private void AllChange()
    {
        OnChangeCurHp?.Invoke(CurHp);
        OnChangeCurStamina?.Invoke(CurStamina);
        OnChangeAttackPower?.Invoke(AttackPower);
        OnChangeFixedStat?.Invoke(fixedstats);
        OnChangePerStat?.Invoke(statPers);
    }
    public void RemoveAllEvent()
    {
        OnChangeLevel = null;
        OnChangeCurHp = null;
        OnChangeCurStamina = null;
        OnChangeAttackPower = null;
        OnChangeFixedStat = null;
        OnChangePerStat = null;
    }

    public void LevelUp()
    {
        level++;
        OnChangeLevel?.Invoke(level);
        for (int i = (int)Utill.RandomRange(1, 3); i > 0; i--)
        {
            if (Utill.IsRandom(50))
            {
                EStat eStat = (EStat)Utill.RandomRange(0, (int)EStat.Size);
                float value = Utill.RandomRange(1, 10);
                SetFixedStat(eStat , value);
                Debug.Log($"{eStat}이 {value}상승했다.");
            }
            else
            {
                EStat eStat = (EStat)Utill.RandomRange(0, (int)EStat.Size);
                float value = Utill.RandomRange(1, 10);
                SetPerStat(eStat, value);
                Debug.Log($"{eStat}이 {value}%상승했다.");
            }
        }
    }
}
