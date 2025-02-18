using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] GearBase weapon;
    [SerializeField] GearBase hat;

    private PlayerController controller;
    private PlayableStat stat;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        stat = controller.StatController.Stat;
    }
    [ContextMenu("test")]
    private void Test()
    {
        SetGear(weapon);
    }

    private void SetGear(GearBase gear)
    {
        List<WeaponFixedStat> fixedStats = new();
        List<WeaponPerStat> perStats = new();
        fixedStats.AddRange(gear.FixedStats);
        perStats.AddRange(gear.PerStats);
        foreach (var item in gear.GearUpgrade.Upgrades.GetRange(0, gear.CurUpgrade))
        {
            fixedStats.AddRange(item.FixedUpStats);
            perStats.AddRange(item.PerUpStats);
        }
        foreach (var item in fixedStats)
        {
            stat.SetFixedStat(item.FixedStat, item.Value);
        }
        foreach (var item in perStats)
        {
            stat.SetPerStat(item.PerStat, item.Value);
        }
    }
}
