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
    [Header("����̸�")]
    public Part Part;
    public string GearName;
    [Header("��� �⺻ �ɷ�ġ")]
    public List<WeaponFixedStat> FixedStats;
    public List<WeaponPerStat> PerStats;
    [Header("��ȭ ������ �� ��ȭ ����")]
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
