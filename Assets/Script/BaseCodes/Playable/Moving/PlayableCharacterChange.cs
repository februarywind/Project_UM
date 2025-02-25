using Unity.Behavior;
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

        cameraController.CameraFollowChange(after == uController ? uCameraTarget : mCameraTarget);

        before.GetComponent<BehaviorGraphAgent>().enabled = true;
        after.GetComponent<BehaviorGraphAgent>().enabled = false;

        before.PlayerFSM.ChangeState(EPlayerState.Atack);
        after.PlayerFSM.ChangeState(EPlayerState.Idle);

        before.enabled = false;
        after.enabled = true;

        after.StatController.StaminaRegen();
    }
}
