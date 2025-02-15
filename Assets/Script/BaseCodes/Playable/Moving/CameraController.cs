using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineBrain brain;
    private CinemachineCamera cinemachineCamera;
    private CinemachineOrbitalFollow orbitalFollow;

    private void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
        cinemachineCamera = brain.ActiveVirtualCamera as CinemachineCamera;
        orbitalFollow = cinemachineCamera.GetComponent<CinemachineOrbitalFollow>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None: CursorLockMode.Locked;
        }
    }

    public void CameraFollowChange(Transform transform)
    {
        cinemachineCamera.Follow = transform;
    }
}
