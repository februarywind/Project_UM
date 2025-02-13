using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAtackData", menuName = "Scriptable Objects/MeleeAtackData")]
public class MeleeAtackData : ScriptableObject
{
    public List<MeleeData> MeleeDatas = new();
    public LayerMask TargetLayer;
    public int DrawCombo;
}

[Serializable]
public class MeleeData
{
    public float Damage;
    public float Range;
    public float Angle;
}