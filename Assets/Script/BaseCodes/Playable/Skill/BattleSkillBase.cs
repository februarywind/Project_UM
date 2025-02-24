using System.Collections;
using UnityEngine;

public class BattleSkillBase : MonoBehaviour
{
    [SerializeField] protected PlayerSkillData skillData;
    public bool IsCoolTime { get; private set; }
    public SkillCoolData skillCoolData { get; private set; } = new();

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
        skillCoolData.CoolTime = skillData.CoolTime;
    }
    public virtual void BattleSkillActivate()
    {
        Debug.Log("전투 스킬 사용");
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
        skillCoolData.OnSkillTime = Time.time;
        playerController.UIController.StatView.UI_SkillCool(skillCoolData.CoolTime, skillCoolData.OnSkillTime, false, IsCoolTime);
        yield return Util.GetDelay(skillData.CoolTime);
        IsCoolTime = false;
    }
}
