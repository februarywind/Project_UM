using UnityEngine;
using UnityEngine.EventSystems;

public class UI_JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private UI_Joystick joystickUI;

    // �巡�� ���� �� ���̽�ƽ ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        joystickUI.OnJoystick(eventData);
    }

    // �巡�� ���� �� �÷��̾� �̵�
    public void OnDrag(PointerEventData eventData)
    {
        joystickUI.JoystickDrag(eventData);
    }

    // �巡�� ���� �� ���̽�ƽ ��Ȱ��ȭ
    public void OnEndDrag(PointerEventData eventData)
    {
        joystickUI.OffJoystick();
    }
}
