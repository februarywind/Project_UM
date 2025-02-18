using System;
using System.Collections.Generic;
using UnityEngine;
public enum Part
{
    Weapon, Hat
}

[CreateAssetMenu(fileName = "GearBase", menuName = "Scriptable Objects/GearBase")]
public class GearBase : ScriptableObject
{
    [Header("장비이름")]
    public Part Part;
    public string GearName;
    [Header("장비 기본 능력치")]
    public List<WeaponFixedStat> FixedStats;
    public List<WeaponPerStat> PerStats;
    [Header("강화 데이터 및 강화 상태")]
    public GearUpgradeBase GearUpgrade;
    public int CurUpgrade;
}
[Serializable]
public class WeaponPerStat
{
    public PerStat PerStat;
    public float Value;
}
[Serializable]
public class WeaponFixedStat
{
    public FixedStat FixedStat;
    public float Value;
}
