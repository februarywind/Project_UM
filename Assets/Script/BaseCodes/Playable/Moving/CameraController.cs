using System.Collections;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineBrain brain;
    private CinemachineCamera cinemachineCamera;
    private CinemachineOrbitalFollow orbitalFollow;
    private CinemachineInputAxisController inputAxisController;

    private Transform follow;

    private async void Start()
    {
        // 시네머신이 한번에 안 가져와져서 딜레이를 넣음
        await Task.Delay(100);

        brain = Camera.main.GetComponent<CinemachineBrain>();
        cinemachineCamera = brain.ActiveVirtualCamera as CinemachineCamera;
        orbitalFollow = cinemachineCamera.GetComponent<CinemachineOrbitalFollow>();
        inputAxisController = cinemachineCamera.GetComponent<CinemachineInputAxisController>();
        follow = cinemachineCamera.Follow;
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
        follow = transform;
    }
    public void InventoryCameraPos(PlayerController controller, bool isReset)
    {
        controller.PlayerRotate(-transform.forward.RemoveOne(RemoveDir.Y));
        orbitalFollow.RadialAxis.Value = isReset ? 1 : 0.4f;
    }

    public void CameraInput(bool isOn)
    {
        inputAxisController.enabled = isOn;
    }
}
