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

        // 이전 캐릭터 스탯 변동 시 UI가 변하지 않도록 이벤트 제거
        before.StatController.Stat.RemoveAllEvent();

        // 이전 캐릭터에서 카메라 타겟을 변경시키고 Behavior를 활성화 시킨다.
        cameraController.CameraFollowChange(after == uController ? uCameraTarget : mCameraTarget);
        before.GetComponent<BehaviorGraphAgent>().enabled = true;
        after.GetComponent<BehaviorGraphAgent>().enabled = false;

        before.PlayerFSM.ChangeState(EPlayerState.Atack);
        after.PlayerFSM.ChangeState(EPlayerState.Idle);

        // 플레이어 컨트롤러 활성화 상태 변경
        before.enabled = false;
        after.enabled = true;

        after.StatController.StaminaRegen();
    }
}
