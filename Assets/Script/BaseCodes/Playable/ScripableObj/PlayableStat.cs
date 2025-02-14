using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBattleStat", menuName = "Scriptable Objects/PlayerBattleStat")]
public class PlayableStat : ScriptableObject
{
    [SerializeField] float maxHp;
    public float MaxHp 
    { 
        get => maxHp + (maxHp * maxHpPer * 0.01f); 
        set
        {
            maxHp = value;
            OnChangeMaxHp?.Invoke(MaxHp);
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

    [SerializeField] float maxHpPer;
    public float MaxHpPer 
    { 
        get => maxHpPer; 
        set
        {
            maxHpPer = value;
            OnChangeMaxHpPer?.Invoke(MaxHpPer);
        }
    }

    public event Action<float> OnChangeMaxHp;
    public event Action<float> OnChangeMaxHpPer;
    public event Action<float> OnChangeCurHp;
}
