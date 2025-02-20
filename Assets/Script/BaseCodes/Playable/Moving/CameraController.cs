using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineBrain brain;
    private CinemachineCamera cinemachineCamera;
    private CinemachineOrbitalFollow orbitalFollow;
    private CinemachineInputAxisController inputAxisController;

    private Transform follow;

    private void Start()
    {
        // 시네머신 컴포넌트가 Start에서 안 가져와짐 왜일까. 
        StartCoroutine(LateStart());
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

    IEnumerator LateStart()
    {
        yield return Utill.GetDelay(0.5f);
        brain = Camera.main.GetComponent<CinemachineBrain>();
        cinemachineCamera = brain.ActiveVirtualCamera as CinemachineCamera;
        orbitalFollow = cinemachineCamera.GetComponent<CinemachineOrbitalFollow>();
        inputAxisController = cinemachineCamera.GetComponent<CinemachineInputAxisController>();
        follow = cinemachineCamera.Follow;
    }
}
