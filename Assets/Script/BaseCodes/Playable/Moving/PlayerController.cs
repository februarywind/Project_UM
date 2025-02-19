using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 입력 관련 변수
    public Vector3 InputDir { get; private set; }
    public bool IsInput { get; private set; }

    // 캐릭터 상태 및 애니메이션
    public Animator Animator { get; private set; }
    public PlayerFSM PlayerFSM { get; private set; }

    // 캐릭터 컨트롤러 및 카메라
    public CharacterController characterController { get; private set; }
    public PlayableCharacterChange CharacterChange => characterChange;
    private PlayableCharacterChange characterChange;
    private Camera cam;

    // 상태 관리
    public EPlayerState[] ConvertibleStates;
    private Dictionary<EPlayerState, Action> stateHandlerDic = new();

    // 물리 및 이동 관련
    [SerializeField] private LayerMask groundLayer;
    private float gravityVelocity;

    // 스킬 및 스탯
    public PlayerMovingStat CharacterMovingStat => movingStat;
    public PlayableStatController StatController => statController;
    public UIController UIController => uIController;

    [SerializeField] BattleSkillBase battleSkill;
    [SerializeField] UltimateSkillBase ultimateSkill;
    [SerializeField] PlayerMovingStat movingStat;
    [SerializeField] PlayableStatController statController;
    private UIController uIController;

    // 코루틴 관리
    private Coroutine rotateCoroutine;
    private Coroutine blendCoroutine;
    private Coroutine dashCoroutine;

    private void Awake()
    {
        cam = Camera.main;
        Animator = GetComponent<Animator>();
        uIController = transform.parent.GetComponentInChildren<UIController>();
        characterController = GetComponent<CharacterController>();
        characterChange = GetComponentInParent<PlayableCharacterChange>();
        PlayerFSM = new(this, movingStat, Animator, battleSkill, ultimateSkill);

        stateHandlerDic.Add(EPlayerState.Idle, IdleHandler);
        stateHandlerDic.Add(EPlayerState.Walk, WalkHandler);
        stateHandlerDic.Add(EPlayerState.Dash, () => DashHandler());
        stateHandlerDic.Add(EPlayerState.Atack, () => AtackHandler());
        stateHandlerDic.Add(EPlayerState.Jump, () => JumpHandler());
        stateHandlerDic.Add(EPlayerState.BattleSkill, BattleSkillHandler);
        stateHandlerDic.Add(EPlayerState.UltimateSkill, UltimateSkillHandler);
        stateHandlerDic.Add(EPlayerState.Change, ChangeHandler);
    }

    private void OnEnable()
    {
        // 플레이어 컨트롤러는 게임 시작과 캐릭터 변경시 OnEnable되므로 아래에 해당 메서드를 넣었음
        uIController.StatView.CharacterChange(this, ultimateSkill, battleSkill);
    }

    private void ChangeHandler()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerFSM.ChangeState(EPlayerState.Change);
        }
    }

    private void UltimateSkillHandler()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerFSM.ChangeState(EPlayerState.UltimateSkill);
        }
    }

    private void Update()
    {
        // 이동 방향을 입력받은 추후 핸들러로 수정해야 할 듯
#if UNITY_EDITOR
        SetMoveDir();
#endif
        // 변경 가능한 상태들을 확인
        ConvertibleStateCheck();
        // 상태 
        PlayerFSM.OnUpdate();
        // 중력 처리
        Gravity();
    }

    public void SetMoveDir(float x = 0, float z = 0)
    {
        // 입력값 받기
#if UNITY_EDITOR
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
#else
        float moveX = x;
        float moveZ = z;
#endif
        // 카메라 기준 이동 방향 계산 (Y축 빼기)
        Vector3 camForward = cam.transform.forward.RemoveOne(RemoveDir.Y).normalized;
        Vector3 camRight = cam.transform.right.RemoveOne(RemoveDir.Y).normalized;

        // 최종 이동 방향 계산
        InputDir = (camForward * moveZ + camRight * moveX).normalized;

        IsInput = InputDir != Vector3.zero;
    }

    private void ConvertibleStateCheck()
    {
        if (ConvertibleStates[0] == EPlayerState.Stop)
            return;

        foreach (var item in ConvertibleStates)
        {
            stateHandlerDic[item].Invoke();
        }
    }

    private void IdleHandler()
    {
        if (!IsInput)
            PlayerFSM.ChangeState(EPlayerState.Idle);
    }

    private void WalkHandler()
    {
        if (IsInput)
            PlayerFSM.ChangeState(EPlayerState.Walk);
    }

    public void DashHandler(bool button = false)
    {
        if (!(Input.GetKeyDown(KeyCode.LeftShift) || button) || dashCoroutine != null)
            return;
        PlayerFSM.ChangeState(EPlayerState.Dash);
        dashCoroutine = StartCoroutine(DashStart(IsInput ? InputDir : transform.forward));
    }

    public void JumpHandler(bool button = false)
    {
        if ((Input.GetKeyDown(KeyCode.Space) || button) && IsGround())
        {
            gravityVelocity = movingStat.JumpHeight;
            PlayerFSM.ChangeState(EPlayerState.Jump);
        }
    }

    public void AtackHandler(bool button = false)
    {
        if (button)
        {
            PlayerFSM.ChangeState(EPlayerState.Atack);
        }
    }

    private void BattleSkillHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerFSM.ChangeState(EPlayerState.BattleSkill);
        }
    }

    public void PlayerMove(float speed, bool isRotate = true)
    {
        characterController.Move(((InputDir * speed) + Vector3.up * gravityVelocity) * Time.deltaTime);
        if (isRotate)
            PlayerRotate(InputDir);
    }

    public void PlayerRotate(Vector3 moveDir)
    {
        // 캐릭터 회전 (움직이는 방향을 바라보도록)
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        rotateCoroutine = StartCoroutine(CharacterRotate(moveDir));
    }

    private void Gravity()
    {
        // 공중에 있을 때 아래로 이동시킴, 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            gravityVelocity = 0;
        }
        else
        {
            gravityVelocity += movingStat.Gravity * Time.deltaTime;
        }
    }

    public void MoveAnimationPlay(float value)
    {
        // 애니메이션 설정
        if (blendCoroutine != null)
            StopCoroutine(blendCoroutine);
        blendCoroutine = StartCoroutine(SetBlendValue(value));
    }

    public bool IsGround()
    {
        return characterController.isGrounded || Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer);
    }

    IEnumerator DashStart(Vector3 dir)
    {
        float elapsedTime = 0;
        PlayerRotate(dir);
        while (elapsedTime < movingStat.DashTime)
        {
            characterController.Move(((dir * movingStat.DashSpeed) + Vector3.up * gravityVelocity) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dashCoroutine = null;
        PlayerFSM.ChangeState(IsInput ? EPlayerState.Run : EPlayerState.Idle);
    }

    IEnumerator CharacterRotate(Vector3 RotateDir)
    {
        float angle = 0;
        // 두 벡터의 사이의 각을 구한다.
        float targetAngle = Vector3.Angle(transform.forward, RotateDir);
        // 오른손 법칙을 기억해, forward 기준으로 오른쪽이면 up, 왼쪽은 down이다.
        Vector3 axis = Vector3.Cross(transform.forward, RotateDir);

        // 너무 작은 각은 움직이지 않게
        if (targetAngle < 2f)
        {
            rotateCoroutine = null;
            yield break;
        }
        while (angle < targetAngle)
        {
            float angleSpeed = movingStat.RotateSpeed * Time.deltaTime;

            // 기준점, 회전축, 속도
            transform.RotateAround(transform.position, axis, angleSpeed);

            angle += angleSpeed;

            yield return null;
        }

        rotateCoroutine = null;
    }

    IEnumerator SetBlendValue(float value)
    {
        float nowValue = Animator.GetFloat("Speed");

        // 이미 해당 값이면 리턴
        if (nowValue == value)
            yield break;

        // 상승인가 하강인가.
        bool isUp = (value - nowValue) > 0;

        while (Mathf.Abs(value - nowValue) > 0.1f)
        {
            Animator.SetFloat("Speed", nowValue);

            // 왜 12로 했더라? 그냥 적당한 값인가?
            nowValue += 12 * Time.deltaTime * (isUp ? 1 : -1);

            yield return null;
        }
        Animator.SetFloat("Speed", value);
        blendCoroutine = null;
    }

    IEnumerator InputCheck(int frame)
    {
        IsInput = true;
        for (int i = 0; i < frame; i++)
        {
            yield return null;
        }
        IsInput = false;
    }

    // 애니메이션 이벤트
    private void ComboEnd()
    {
        PlayerFSM.ChangeState(PlayerFSM.BeforeState == EPlayerState.Run ? EPlayerState.Run : EPlayerState.Idle);
    }
}