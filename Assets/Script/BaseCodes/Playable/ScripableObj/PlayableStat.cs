using System;
using UnityEngine;

public enum StatPer
{
    MaxHp, MaxStamina, AttackPower, Size
}

[CreateAssetMenu(fileName = "PlayerBattleStat", menuName = "Scriptable Objects/PlayerBattleStat")]
public class PlayableStat : ScriptableObject
{
    [SerializeField] float[] statPers = new float[(int)StatPer.Size];

    [SerializeField] float maxHp;
    public float MaxHp 
    { 
        get => maxHp + (maxHp * GetPerStat(StatPer.MaxHp) * 0.01f); 
        set
        {
            maxHp = value;
            OnChangeCurHp?.Invoke(CurHp);
        }
    }

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

    [SerializeField] float maxStamina;
    public float MaxStamina
    { 
        get => maxStamina + (maxStamina * GetPerStat(StatPer.MaxStamina) * 0.01f); 
        set
        {
            maxStamina = value;
            OnChangeCurStamina?.Invoke(CurStamina);
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

    [SerializeField] float attackPower;
    public float AttackPower
    { 
        get => attackPower + (attackPower * GetPerStat(StatPer.AttackPower) * 0.01f); 
        set
        {
            attackPower = value;
            OnChangeAttackPower?.Invoke(AttackPower);
        }
    }

    public void SetPerStat(StatPer statPer, float value)
    {
        statPers[(int)statPer] += value;
        OnChangePerStat?.Invoke(statPers);
    }
    public float GetPerStat(StatPer statPer)
    {
        return statPers[(int)statPer];
    }

    public event Action<float> OnChangeCurHp;
    public event Action<float> OnChangeCurStamina;
    public event Action<float> OnChangeAttackPower;
    public event Action<float[]> OnChangePerStat;
}
