using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatView : MonoBehaviour
{
    // 패널
    [SerializeField] GameObject statPanel;
    // 슬라이더
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider staminaSlider;
    // 텍스트
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text statPointText;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text staminaText;
    [SerializeField] TMP_Text attackPowerText;
    // 스킬 쿨 이미지
    [SerializeField] Image ultimateCoolImage;
    [SerializeField] Image battleCoolImage;
    // 스탯 업 버튼
    [SerializeField] Button maxHpUpButton;
    [SerializeField] Button maxStaminaUpButton;
    [SerializeField] Button attackUpButton;

    private PlayableStat stat;

    private Coroutine ultimateCoolCoroutine;
    private Coroutine battleCoolCoroutine;

    public void CharacterChange(PlayerController controller, UltimateSkillBase ultimate, BattleSkillBase battle)
    {
        stat = controller.StatController.Stat;

        stat.OnChangeCurHp += Stat_OnChangeCurHp;
        Stat_OnChangeCurHp(stat.CurHp);

        stat.OnChangeAttackPower += Stat_OnChangeAttackPower;
        Stat_OnChangeAttackPower(stat.AttackPower);

        stat.OnChangeCurStamina += PlayableStat_OnChangeCurStamina;

        stat.OnChangeLevel += Stat_OnChangeLevel;
        Stat_OnChangeLevel(stat.Level);

        UI_SkillCool(ultimate.skillCoolData.CoolTime, ultimate.skillCoolData.OnSkillTime, true, ultimate.IsCoolTime);
        UI_SkillCool(battle.skillCoolData.CoolTime, battle.skillCoolData.OnSkillTime, false, battle.IsCoolTime);

        maxHpUpButton.onClick.RemoveAllListeners();
        maxHpUpButton.onClick.AddListener(() => StatUp(EStat.MaxHp));
        maxStaminaUpButton.onClick.RemoveAllListeners();
        maxStaminaUpButton.onClick.AddListener(() => StatUp(EStat.MaxStamina));
        attackUpButton.onClick.RemoveAllListeners();
        attackUpButton.onClick.AddListener(() => StatUp(EStat.AttackPower));
    }

    private void Stat_OnChangeLevel(int level)
    {
        levelText.text = $"LV {level}";
        statPointText.text = $"SP: {stat.StatPoint}";
    }

    private void PlayableStat_OnChangeCurStamina(float curStamina)
    {
        staminaSlider.maxValue = stat.MaxStamina;
        staminaSlider.value = curStamina;
        staminaText.text = $"Stamina : {curStamina:F1}/{stat.MaxStamina:F1}";
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

    public void SetPanel(bool isOpen)
    {
        statPanel.SetActive(isOpen);
    }

    private void Stat_OnChangeAttackPower(float attackPower)
    {
        attackPowerText.text = $"Attack Power: {attackPower:F1}";
    }

    private void Stat_OnChangeCurHp(float curHp)
    {
        hpSlider.maxValue = stat.MaxHp;
        hpSlider.value = curHp;

        hpText.text = $"HP : {curHp:F1} / {stat.MaxHp:F1}";
    }

    private void StatUp(EStat eStat)
    {
        stat.StatUp(eStat);
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
}
