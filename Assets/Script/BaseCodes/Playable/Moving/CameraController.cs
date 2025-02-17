using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineBrain brain;
    private CinemachineCamera cinemachineCamera;
    private CinemachineOrbitalFollow orbitalFollow;
    private CinemachineInputAxisController inputAxisController;

    private void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
        cinemachineCamera = brain.ActiveVirtualCamera as CinemachineCamera;
        orbitalFollow = cinemachineCamera.GetComponent<CinemachineOrbitalFollow>();
        inputAxisController = cinemachineCamera.GetComponent<CinemachineInputAxisController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void CameraFollowChange(Transform transform)
    {
        cinemachineCamera.Follow = transform;
    }
    public void LookAtCharacter(bool isForward, float radialValue)
    {
        float t = Vector3.Angle(isForward ? cinemachineCamera.Follow.forward : -cinemachineCamera.Follow.forward, (transform.position.RemoveOne(RemoveDir.Y) - cinemachineCamera.Follow.position.RemoveOne(RemoveDir.Y)).normalized);
        orbitalFollow.HorizontalAxis.Value += t * (orbitalFollow.HorizontalAxis.Value > 0 ? -1 : 1);
        orbitalFollow.RadialAxis.Value = radialValue;
    }

    public void CameraInput(bool isOn)
    {
        inputAxisController.enabled = isOn;
    }
}
