using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatView : MonoBehaviour
{
    [SerializeField] GameObject statPanel;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider staminaSlider;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text staminaText;
    [SerializeField] TMP_Text attackPowerText;
    [SerializeField] Image ultimateCoolImage;
    [SerializeField] Image battleCoolImage;

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

        UI_SkillCool(ultimate.skillCoolData.CoolTime, ultimate.skillCoolData.OnSkillTime, true, ultimate.IsCoolTime);
        UI_SkillCool(battle.skillCoolData.CoolTime, battle.skillCoolData.OnSkillTime, false, battle.IsCoolTime);
    }

    private void PlayableStat_OnChangeCurStamina(float curStamina)
    {
        staminaSlider.maxValue = stat.MaxStamina;
        staminaSlider.value = curStamina;
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
        attackPowerText.text = $"Attack Power: {attackPower}";
    }

    private void Stat_OnChangeCurHp(float curHp)
    {
        hpSlider.maxValue = stat.MaxHp;
        hpSlider.value = curHp;

        hpText.text = $"HP : {curHp} / {stat.MaxHp}";
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
