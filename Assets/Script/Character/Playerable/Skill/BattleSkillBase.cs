using System.Collections;
using UnityEngine;

public class BattleSkillBase : MonoBehaviour
{
    public bool IsCoolTime { get; private set; }

    protected PlayerFSM playerFSM;
    protected PlayerController playerController;

    [SerializeField] float coolTime;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerFSM = playerController.PlayerFSM;
    }
    public virtual void BattleSkillActivate()
    {
        Debug.Log("전투 스킬 사용");
    }

    public virtual void SkillCoolTime() 
    {
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        IsCoolTime = true;
        yield return Utill.GetDelay(coolTime);
        IsCoolTime = false;
    }
}
