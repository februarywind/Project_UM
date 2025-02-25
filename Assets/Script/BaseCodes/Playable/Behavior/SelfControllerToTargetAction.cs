using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelfControllerToTarget", story: "[Self] ControllerMove To [Target]", category: "Action", id: "6c3a99615c1826376a51afe8638c480a")]
public partial class SelfControllerToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    [SerializeReference] public BlackboardVariable<OtherPlayableState> CurState;
    [SerializeReference] public BlackboardVariable<OtherPlayableState> State;

    [SerializeReference] public BlackboardVariable<float> Speed = new(1);
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.2f);
    [SerializeReference] public BlackboardVariable<string> AnimatorSpeedParam = new("Speed");
    [SerializeReference] public BlackboardVariable<float> AnimatorValue = new(1f);

    private CharacterController controller;
    private PlayerController playerController;

    protected override Status OnStart()
    {
        return Init();
    }

    protected override Status OnUpdate()
    {
        Self.Value.transform.forward = Target.Value.transform.position.RemoveOne(RemoveDir.Y) - Self.Value.transform.position.RemoveOne(RemoveDir.Y);
        controller.Move(((Self.Value.transform.forward * Speed) + (Vector3.up * -9.81f)) * Time.deltaTime);
        if (State.Value != CurState)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        // 플레이모드 중지 시 OnEnd가 실행되어 코루틴 에러가 발생, 예외처리 함
        if (State.Value == CurState) return;
        playerController.MoveAnimationPlay(0);
    }
    private Status Init()
    {
        controller = Self.Value.GetComponent<CharacterController>();
        playerController = controller.GetComponent<PlayerController>();
        if (!controller || !playerController)
        {
            return Status.Failure;
        }
        playerController.Animator.Play("Blend Tree", 0);
        playerController.MoveAnimationPlay(AnimatorValue);
        return Status.Running;
    }
}

