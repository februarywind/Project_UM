using System.Collections.Generic;
using UnityEngine;
public enum RemoveDir
{
    X, Y, Z
}

public static class Util
{
    public static Vector3 RemoveOne(this Vector3 vector3, RemoveDir removeDir)
    {
        switch (removeDir)
        {
            case RemoveDir.X:
                vector3.x = 0;
                break;
            case RemoveDir.Y:
                vector3.y = 0;
                break;
            case RemoveDir.Z:
                vector3.z = 0;
                break;
        }
        return vector3;
    }

    private static Dictionary<float, WaitForSeconds> delayMap = new();
    public static WaitForSeconds GetDelay(float delay)
    {
        if (delayMap.ContainsKey(delay))
        {
            return delayMap[delay];
        }
        delayMap.Add(delay, new WaitForSeconds(delay));
        return delayMap[delay];
    }
    public static float RandomRange(float min, float max)
    {
        return Random.Range(min, max);
    }
    public static bool IsRandom(float Ratio)
    {
        return Random.Range(1, 100) < Ratio;
    }
}