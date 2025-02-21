using UnityEngine;

public class PlayableCharacterChange : MonoBehaviour
{
    public PlayerController CurController { get; private set; }

    [SerializeField] PlayerController uController;
    [SerializeField] Transform uCameraTarget;
    [SerializeField] PlayerController mController;
    [SerializeField] Transform mCameraTarget;

    private CameraController cameraController;
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        CurController = mController;
    }

    public void ChangeCharacter(PlayerController curController)
    {
        PlayerController before = curController;
        PlayerController after = curController == uController ? mController : uController;
        CurController = after;

        before.StatController.Stat.RemoveAllEvent();

        after.transform.position = before.transform.position;
        after.transform.rotation = before.transform.rotation;

        before.PlayerFSM.ChangeState(EPlayerState.Atack);

        before.gameObject.SetActive(false);
        after.gameObject.SetActive(true);

        after.PlayerFSM.ChangeState(EPlayerState.Idle);

        cameraController.CameraFollowChange(after == uController ? uCameraTarget : mCameraTarget);

        after.StatController.StaminaRegen();
    }
}
