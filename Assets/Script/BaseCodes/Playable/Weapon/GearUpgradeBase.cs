using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearUpgradeBase", menuName = "Scriptable Objects/GearUpgradeBase")]
public class GearUpgradeBase : ScriptableObject
{
    public List<GearUpgradeStat> Upgrades;
}
[Serializable]
public class GearUpgradeStat
{
    public List<WeaponFixedStat> FixedUpStats;
    public List<WeaponPerStat> PerUpStats;
}
