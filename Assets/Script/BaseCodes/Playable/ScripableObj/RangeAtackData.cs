using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeAtackData", menuName = "Scriptable Objects/RangeAtackData")]
public class RangeAtackData : ScriptableObject
{
    public List<RangeData> RangeDatas = new();
    public LayerMask TargetLayer;
    public int DrawCombo;
}
[Serializable]
public class RangeData
{
    public float Damage;
    public float Range;
    public float Radius;
}
