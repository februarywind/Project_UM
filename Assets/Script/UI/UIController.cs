using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject statCanvas;
    [SerializeField] Slider hpSlider;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text staminaText;
    [SerializeField] TMP_Text attackPowerText;
    [SerializeField] Image ultimateCoolImage;
    [SerializeField] Image battleCoolImage;

    private PlayableStat playableStat;

    private Coroutine ultimateCoolCoroutine;
    private Coroutine battleCoolCoroutine;

    public void CharacterChange(PlayableStat stat, UltimateSkillBase ultimate, BattleSkillBase battle)
    {
        playableStat = stat;

        stat.OnChangeCurHp -= Stat_OnChangeCurHp;
        stat.OnChangeCurHp += Stat_OnChangeCurHp;
        Stat_OnChangeCurHp(stat.CurHp);

        stat.OnChangeAttackPower += Stat_OnChangeAttackPower;
        stat.OnChangeAttackPower += Stat_OnChangeAttackPower;
        Stat_OnChangeAttackPower(stat.AttackPower);

        UI_SkillCool(ultimate.skillCoolData.CoolTime, ultimate.skillCoolData.OnSkillTime, true, ultimate.IsCoolTime);
        UI_SkillCool(battle.skillCoolData.CoolTime, battle.skillCoolData.OnSkillTime, false, battle.IsCoolTime);
    }
    public void UI_SkillCool(float coolTime, float onSkillTime, bool isUltimate, bool isCoolTime)
    {
        if (isUltimate)
        {
            if (ultimateCoolCoroutine != null)
                StopCoroutine(ultimateCoolCoroutine);
            ultimateCoolCoroutine = StartCoroutine(SkillCoolImg(coolTime, onSkillTime, isUltimate, isCoolTime));
        }
        else
        {
            if (battleCoolCoroutine != null)
                StopCoroutine(battleCoolCoroutine);
            battleCoolCoroutine = StartCoroutine(SkillCoolImg(coolTime, onSkillTime, isUltimate, isCoolTime));
        }
    }

    private void Stat_OnChangeAttackPower(float attackPower)
    {
        attackPowerText.text = $"Attack Power: {attackPower}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            statCanvas.SetActive(!statCanvas.activeSelf);
        }
    }

    private void Stat_OnChangeCurHp(float curHp)
    {
        hpSlider.maxValue = playableStat.MaxHp;
        hpSlider.value = curHp;

        hpText.text = $"HP : {curHp} / {playableStat.MaxHp}";
    }

    IEnumerator SkillCoolImg(float coolTime, float onSkillTime, bool isUltimate, bool isCoolTime)
    {
        float multy = 1 / coolTime;
        float remainingTime = coolTime - (Time.time - onSkillTime);
        Image image = isUltimate ? ultimateCoolImage : battleCoolImage;
        while (isCoolTime && remainingTime > 0)
        {
            image.fillAmount = (coolTime - remainingTime) * multy;
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        image.fillAmount = 1;
    }

    [ContextMenu("levelup")]
    private void TempLevelUp()
    {
        playableStat.SetPerStat(PerStat.MaxHp, 10);
        playableStat.SetFixedStat(FixedStat.MaxHp, 10);
    }
}
