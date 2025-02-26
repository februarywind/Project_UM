using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 입력 관련 변수
    public Vector3 InputDir { get; private set; }
    public bool IsInput { get; private set; }
    public bool IsControl = true;

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

    // 점프 및 낙하
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
        stateHandlerDic.Add(EPlayerState.Change, () => ChangeHandler());

        //저프레임 테스트
        //Application.targetFrameRate = 40;
    }

    private void OnEnable()
    {
        // 플레이어 컨트롤러는 게임 시작과 캐릭터 변경시 OnEnable되므로 아래에 해당 메서드를 넣었음
        uIController.StatView.CharacterChange(this, ultimateSkill, battleSkill);

        // 대쉬 쿨 타임 중 캐릭터 변경시 코루틴 초기화가 안됨
        dashCoroutine = null;
    }

    public void ChangeHandler(bool button = false)
    {
        if (Input.GetKeyDown(KeyCode.Tab) || button)
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
        if (!IsControl)
        {
            return;
        }

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
        if (!(Input.GetKeyDown(KeyCode.LeftShift) || button) || dashCoroutine != null || StatController.Stat.CurStamina < 1)
            return;
        PlayerFSM.ChangeState(EPlayerState.Dash);
        dashCoroutine = StartCoroutine(DashStart(IsInput ? InputDir : transform.forward));

        StatController.Stat.CurStamina -= statController.Stat.DashStaminaValue;
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
        if (button || (Cursor.lockState == CursorLockMode.Locked && Input.GetMouseButtonDown(0)))
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
        // 두 벡터의 사이의 각을 구한다.
        float targetAngle = Vector3.Angle(transform.forward, moveDir);
        // 오른손 법칙을 기억해, forward 기준으로 오른쪽이면 up, 왼쪽은 down이다.
        Vector3 axis = Vector3.Cross(transform.forward, moveDir);

        // 너무 작은 각은 움직이지 않도록
        if (targetAngle < 10f)
        {
            transform.RotateAround(transform.position, axis, targetAngle);
            return;
        }
        float angleSpeed = movingStat.RotateSpeed * Time.deltaTime;

        // 기준점, 회전축, 속도
        transform.RotateAround(transform.position, axis, angleSpeed);
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
        statController.StaminaRegenStop();

        float elapsedTime = 0;
        transform.RotateAround(transform.position, Vector3.up, Vector3.Angle(transform.forward, dir));
        while (elapsedTime < movingStat.DashTime)
        {
            characterController.Move(((dir * movingStat.DashSpeed) + Vector3.up * gravityVelocity) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        PlayerFSM.ChangeState(IsInput ? EPlayerState.Run : EPlayerState.Idle);

        if (!IsInput)
            statController.StaminaRegen();

        // 코루틴 종료 시간을 미뤄서 대쉬 쿨타임 생성
        yield return Util.GetDelay(movingStat.DashCoolTime);
        dashCoroutine = null;
    }

    IEnumerator SetBlendValue(float value)
    {
        float nowValue = Animator.GetFloat("Speed");

        // 이미 해당 값이면 리턴
        if (nowValue == value)
            yield break;

        // 상승인가 하강인가.
        bool isUp = (value - nowValue) > 0;
        while (isUp ? value > nowValue : value < nowValue)
        {
            Animator.SetFloat("Speed", nowValue);

            // 왜 12로 했더라? 그냥 적당한 값인가?
            nowValue += 12 * Time.deltaTime * (isUp ? 1 : -1);

            yield return null;
        }
        Animator.SetFloat("Speed", value);
        blendCoroutine = null;
    }

    // 애니메이션 이벤트
    private void ComboEnd()
    {
        PlayerFSM.ChangeState(PlayerFSM.BeforeState == EPlayerState.Run ? EPlayerState.Run : EPlayerState.Idle);
    }
}