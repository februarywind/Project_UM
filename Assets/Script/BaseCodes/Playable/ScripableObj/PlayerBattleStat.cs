using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBattleStat", menuName = "Scriptable Objects/PlayerBattleStat")]
public class PlayerBattleStat : ScriptableObject
{
    [SerializeField] float maxHp;
    public float MaxHp => maxHp;

    [SerializeField] float curHp;
    public float CurHp
    {
        get => curHp;
        set 
        { 
            curHp = value; 
            OnChangeCurHp?.Invoke(curHp);
        }
    }

    public event Action<float> OnChangeCurHp;
}
