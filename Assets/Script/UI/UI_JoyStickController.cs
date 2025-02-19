using UnityEngine;
using UnityEngine.EventSystems;

public class UI_JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private UI_Joystick joystickUI;

    // 드래그 시작 시 조이스틱 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        joystickUI.OnJoystick(eventData);
    }

    // 드래그 중일 시 플레이어 이동
    public void OnDrag(PointerEventData eventData)
    {
        joystickUI.JoystickDrag(eventData);
    }

    // 드래그 해제 시 조이스틱 비활성화
    public void OnEndDrag(PointerEventData eventData)
    {
        joystickUI.OffJoystick();
    }
}
