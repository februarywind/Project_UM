using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBattleStat", menuName = "Scriptable Objects/PlayerBattleStat")]
public class PlayerBattleStat : ScriptableObject
{
    [SerializeField] float maxHp;
    public float MaxHp { get { return maxHp; } }

    [SerializeField] float stamina;
    public float Stamina { get { return stamina; } }
}
