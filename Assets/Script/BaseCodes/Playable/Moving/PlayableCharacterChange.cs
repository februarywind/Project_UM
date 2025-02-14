using UnityEngine;

public class PlayableCharacterChange : MonoBehaviour
{
    [SerializeField] PlayerController uController;
    [SerializeField] Transform uCameraTarget;
    [SerializeField] PlayerController mController;
    [SerializeField] Transform mCameraTarget;

    public void ChangeCharacter(PlayerController curController)
    {
        PlayerController before = curController;
        PlayerController after = curController == uController ? mController : uController;

        after.transform.position = before.transform.position;
        after.transform.rotation = before.transform.rotation;

        before.PlayerFSM.ChangeState(EPlayerState.Atack);

        before.gameObject.SetActive(false);
        after.gameObject.SetActive(true);

        after.PlayerFSM.ChangeState(EPlayerState.Idle);

        Camera.main.GetComponent<CameraController>().CameraFollowChange(after == uController ? uCameraTarget : mCameraTarget);
    }
}
