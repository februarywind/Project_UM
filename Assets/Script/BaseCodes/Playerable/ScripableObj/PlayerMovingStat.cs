using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterStat", menuName = "Scriptable Objects/PlayerCharacterStat")]
public class PlayerMovingStat : ScriptableObject
{
    [SerializeField] float walkSpeed;
    public float WalkSpeed => walkSpeed;

    [SerializeField] float runSpeed;
    public float RunSpeed => runSpeed;

    [SerializeField] float jumpHeight;
    public float JumpHeight => jumpHeight;

    [SerializeField] float rotateSpeed;
    public float RotateSpeed => rotateSpeed;

    [SerializeField] float dashTime;
    public float DashTime => dashTime;

    [SerializeField] float dashSpeed;
    public float DashSpeed => dashSpeed;

    [SerializeField] float gravity = -9.81f;
    public float Gravity => gravity;
}
