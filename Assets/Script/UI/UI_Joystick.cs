using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;

public class UI_Joystick : MonoBehaviour
{
    [SerializeField]
    private RectTransform _handle;            // ���� Ʋ �ȿ� �ִ� ���� ��ü

    private Vector2 _touch;             // ��ġ�� ��ġ

    private float _widthHalf;         // ���� Ʋ�� ������

    [SerializeField]
    private float _speed = 0.35f;    // _speed�� Handle��  ��ġ�� �հ����� �Ѵ� �ӵ�

    [SerializeField]
    private float _length = 0.8f;     // _handle�� �ִ� �̵� ����

    private PlayableCharacterChange characterChange;
    private void Start()
    {
        // ���� Ʋ�� ������
        _widthHalf = GetComponent<RectTransform>().sizeDelta.x * 0.5f;

        gameObject.SetActive(false);

        characterChange = transform.root.GetComponent<PlayableCharacterChange>();
    }

    // ���̽�ƽ �ڵ鷯 �巡��
    public void JoystickDrag(PointerEventData eventData)
    {
        // ��ġ�� ��ġ�� ������ ��ġ ���̸� ���ϰ� ���������� �����ֱ�
        _touch = ((eventData.position - (Vector2)transform.position) * _speed) / _widthHalf;

        // ���� Ʋ�� ��ġ�� Handle ���� Ȯ��
        if (_touch.magnitude > _length)
            _touch = _touch.normalized * _length;

        // Handle ��ġ ����
        _handle.anchoredPosition = _touch * _widthHalf;

        _touch = _touch.normalized;
        characterChange.CurController.SetMoveDir(_touch.x, _touch.y);
    }

    // ���콺 ��ġ�� ���̽�ƽ Ȱ��ȭ
    public void OnJoystick(PointerEventData eventData)
    {
        gameObject.SetActive(true);

        transform.position = eventData.position;
    }

    // ���̽�ƽ ��Ȱ��ȭ
    public void OffJoystick()
    {
        gameObject.SetActive(false);

        // ��ġ �ʱ�ȭ
        _touch = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
        characterChange.CurController.SetMoveDir(0, 0);
    }
}