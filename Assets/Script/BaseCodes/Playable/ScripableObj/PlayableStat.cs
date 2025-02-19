using System;
using UnityEngine;

public enum PerStat
{
    MaxHp, MaxStamina, AttackPower, Size
}
public enum FixedStat
{
    MaxHp, MaxStamina, AttackPower, Size
}

[CreateAssetMenu(fileName = "PlayerBattleStat", menuName = "Scriptable Objects/PlayerBattleStat")]
public class PlayableStat : ScriptableObject
{
    [SerializeField] int level;
    public int Level => level;

    [Header("추가 능력치")]
    [SerializeField] float[] statPers = new float[(int)PerStat.Size];
    [SerializeField] float[] fixedstats = new float[(int)FixedStat.Size];

    [Header("최대 능력치")]

    [SerializeField] float maxHp;
    public float MaxHp
    {
        get
        {
            float baseHp = maxHp + fixedstats[(int)FixedStat.MaxHp];
            return baseHp * (1 + GetPerStat(PerStat.MaxHp) * 0.01f);
        }
    }
    [SerializeField] float maxStamina;
    public float MaxStamina
    {
        get
        {
            float baseStamina = maxStamina + fixedstats[(int)FixedStat.MaxStamina];
            return baseStamina * (1 + GetPerStat(PerStat.MaxStamina) * 0.01f);
        }
    }

    [SerializeField] float attackPower;
    public float AttackPower
    {
        get
        {
            float baseAttackPower = attackPower + fixedstats[(int)FixedStat.AttackPower];
            return baseAttackPower * (1 + GetPerStat(PerStat.AttackPower) * 0.01f);
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
    public void SetPerStat(PerStat statPer, float value)
    {
        statPers[(int)statPer] += value;
        AllChange();
    }
    public float GetPerStat(PerStat statPer)
    {
        return statPers[(int)statPer];
    }

    public void SetFixedStat(FixedStat fixedStat, float value)
    {
        fixedstats[(int)fixedStat] += value; 
        AllChange();
    }
    public float GetFixedStat(FixedStat fixedStat)
    {
        return fixedstats[(int)fixedStat];
    }

    public event Action<float> OnChangeCurHp;
    public event Action<float> OnChangeCurStamina;
    public event Action<float> OnChangeAttackPower;
    public event Action<float[]> OnChangePerStat;

    private void AllChange()
    {
        OnChangeCurHp?.Invoke(CurHp);
        OnChangeCurStamina?.Invoke(CurStamina);
        OnChangeAttackPower?.Invoke(AttackPower);
    }
    public void RemoveAllEvent()
    {
        OnChangeCurHp = null;
        OnChangeCurStamina = null;
        OnChangeAttackPower = null;
        OnChangePerStat = null;
    }
}
