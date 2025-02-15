using System.Collections;
using UnityEngine;

public class UltimateSkillBase : MonoBehaviour
{
    [SerializeField] protected PlayerSkillData skillData;
    public bool IsCoolTime { get; private set; }

    protected PlayerController playerController;
    protected PlayerFSM playerFSM;
    protected PlayableStat stat;

    private Coroutine coolCoroutine;

    protected SkillController skillController;

    private void Start()
    {
        skillController = GetComponentInParent<SkillController>();
        playerController = GetComponent<PlayerController>();
        playerFSM = playerController.PlayerFSM;
        stat = playerController.StatController.Stat;
    }
    public virtual void UltimateSkillActivate()
    {
        Debug.Log("궁극기 사용");
    }

    protected void CoolDownStart()
    {
        skillController.CoroutineAgent(CoolDown());
    }

    public void CoolTimeReset()
    {
        StopCoroutine(coolCoroutine);
        IsCoolTime = false;
    }

    private IEnumerator CoolDown()
    {
        IsCoolTime = true;
        yield return Utill.GetDelay(skillData.CoolTime);
        IsCoolTime = false;
    }
}
