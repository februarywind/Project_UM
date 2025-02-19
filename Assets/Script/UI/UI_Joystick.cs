using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;

public class UI_Joystick : MonoBehaviour
{
    [SerializeField]
    private RectTransform _handle;            // 원형 틀 안에 있는 원형 객체

    private Vector2 _touch;             // 터치한 위치

    private float _widthHalf;         // 원형 틀의 반지름

    [SerializeField]
    private float _speed = 0.35f;    // _speed의 Handle이  터치한 손가락을 쫓는 속도

    [SerializeField]
    private float _length = 0.8f;     // _handle의 최대 이동 길이

    private PlayableCharacterChange characterChange;
    private void Start()
    {
        // 원형 틀의 반지름
        _widthHalf = GetComponent<RectTransform>().sizeDelta.x * 0.5f;

        gameObject.SetActive(false);

        characterChange = transform.root.GetComponent<PlayableCharacterChange>();
    }

    // 조이스틱 핸들러 드래그
    public void JoystickDrag(PointerEventData eventData)
    {
        // 터치한 위치와 원형의 위치 차이를 구하고 반지름으로 나눠주기
        _touch = ((eventData.position - (Vector2)transform.position) * _speed) / _widthHalf;

        // 원형 틀에 걸치는 Handle 길이 확인
        if (_touch.magnitude > _length)
            _touch = _touch.normalized * _length;

        // Handle 위치 설정
        _handle.anchoredPosition = _touch * _widthHalf;

        _touch = _touch.normalized;
        characterChange.CurController.SetMoveDir(_touch.x, _touch.y);
    }

    // 마우스 위치에 조이스틱 활성화
    public void OnJoystick(PointerEventData eventData)
    {
        gameObject.SetActive(true);

        transform.position = eventData.position;
    }

    // 조이스틱 비활성화
    public void OffJoystick()
    {
        gameObject.SetActive(false);

        // 위치 초기화
        _touch = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
        characterChange.CurController.SetMoveDir(0, 0);
    }
}