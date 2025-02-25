using Unity.Cinemachine;
using UnityEngine;

public class SkillCamera : MonoBehaviour
{
    [SerializeField] CinemachineCamera statViewCamera;
    [SerializeField] CinemachineCamera ultimateCamera;
    private PlayerController playerController;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void StatViewCamera(bool isPocus)
    {
        statViewCamera.Priority = isPocus ? 1 : -1;
        playerController.IsControl = !isPocus;
        playerController.MoveAnimationPlay(0);
    }

    public void UtimateViewCamera(bool isPocus)
    {
        ultimateCamera.Priority = isPocus ? 1 : -1;
    }
}
