using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StopAction
{
    All, Move, Dash, Gravity, Size
}

public class PlayerController : MonoBehaviour
{
    public bool IsInput { get; private set; }

    [SerializeField] LayerMask groundLayer;
    [SerializeField] PlayerCharacterStat characterStat;

    private Vector3 InputDir;

    private float gravityVelocity;

    private Camera cam;
    private Animator animator;
    private CharacterController characterController;

    private Coroutine rotateCoroutine;
    private Coroutine blendCoroutine;
    private Coroutine dashCoroutine;
    private Coroutine inputCoroutine;

    private PlayerFSM playerFSM;
    public EPlayerState[] ConvertibleStates;
    private Dictionary<EPlayerState, Action> stateM = new();

    private void Awake()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerFSM = new(this, characterStat, animator);


        stateM.Add(EPlayerState.Idle, IdleHandler);
        stateM.Add(EPlayerState.Walk, WalkHandler);
        stateM.Add(EPlayerState.Dash, DashHandler);
    }

    private void Update()
    {
        SetMoveDir();
        ConvertibleStateCheck();
        playerFSM.OnUpdate();
        Gravity();
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
            gravityVelocity += characterStat.Gravity * Time.deltaTime;
        }

        // 캐릭터 컨트롤러는 바닥을 잘 인식 못해서 점프가 잘 안먹힘 Ray추가
        if (Input.GetKeyDown(KeyCode.Space) && (characterController.isGrounded || IsGround()))
        {
            gravityVelocity = characterStat.JumpHeight;
        }
    }

    private void SetMoveDir()
    {
        // 입력값 받기
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 카메라 기준 이동 방향 계산 (Y축 빼기)
        Vector3 camForward = cam.transform.forward.RemoveOne(RemoveDir.Y).normalized;
        Vector3 camRight = cam.transform.right.RemoveOne(RemoveDir.Y).normalized;

        // 최종 이동 방향 계산
        InputDir = (camForward * moveZ + camRight * moveX).normalized;

        if (InputDir != Vector3.zero)
        {
            if (inputCoroutine != null)
                StopCoroutine(inputCoroutine);
            inputCoroutine = StartCoroutine(InputCheck(10));
        }
    }
    public void PlayerMove(float speed)
    {
        characterController.Move(((InputDir * speed) + Vector3.up * gravityVelocity) * Time.deltaTime);
        PlayerRotate(InputDir);
    }

    private void IdleHandler()
    {
        if (!IsInput)
            playerFSM.ChangeState(EPlayerState.Idle);
    }
    private void WalkHandler()
    {
        if (IsInput)
            playerFSM.ChangeState(EPlayerState.Walk);
    }
    private void DashHandler()
    {
        if (!Input.GetKeyDown(KeyCode.LeftShift) || dashCoroutine != null)
            return;
        playerFSM.ChangeState(EPlayerState.Dash);
        dashCoroutine = StartCoroutine(DashStart(IsInput ? InputDir : transform.forward));
    }

    private void PlayerRotate(Vector3 moveDir)
    {
        // 캐릭터 회전 (움직이는 방향을 바라보도록)
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        rotateCoroutine = StartCoroutine(CharacterRotate(moveDir));
    }

    public void MoveAnimationPlay(float value)
    {
        // 애니메이션 설정
        if (blendCoroutine == null)
            blendCoroutine = StartCoroutine(SetBlendValue(value));
    }

    private bool IsGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer);
    }

    IEnumerator DashStart(Vector3 dir)
    {
        float elapsedTime = 0;
        while (elapsedTime < characterStat.DashTime)
        {
            characterController.Move(((dir * characterStat.DashSpeed) + Vector3.up * gravityVelocity) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dashCoroutine = null;
        playerFSM.ChangeState(IsInput ? EPlayerState.Run : EPlayerState.Idle);
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
            float angleSpeed = characterStat.RotateSpeed * Time.deltaTime;

            // 기준점, 회전축, 속도
            transform.RotateAround(transform.position, axis, angleSpeed);

            angle += angleSpeed;

            yield return null;
        }

        rotateCoroutine = null;
    }

    IEnumerator SetBlendValue(float value)
    {
        float nowValue = animator.GetFloat("Speed");

        // 이미 해당 값이면 리턴
        if (nowValue == value)
            yield break;

        // 상승인가 하강인가.
        bool isUp = (value - nowValue) > 0;

        while (Mathf.Abs(value - nowValue) > 0.1f)
        {
            animator.SetFloat("Speed", nowValue);

            // 왜 12로 했더라? 그냥 적당한 값인가?
            nowValue += 12 * Time.deltaTime * (isUp ? 1 : -1);

            yield return null;
        }
        animator.SetFloat("Speed", value);
        blendCoroutine = null;
    }

    // 입력이 없어도 n프레임 동안 상태가 유지
    IEnumerator InputCheck(int frame)
    {
        IsInput = true;
        for (int i = 0; i < frame; i++)
        {
            yield return null;
        }
        IsInput = false;
    }

    private void ConvertibleStateCheck()
    {
        foreach (var item in ConvertibleStates)
        {
            stateM[item].Invoke();
        }
    }
}