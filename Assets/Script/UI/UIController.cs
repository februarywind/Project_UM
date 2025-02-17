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

    private PlayableStat playableStat;

    public void CharacterChange(PlayableStat stat)
    {
        playableStat = stat;

        stat.OnChangeCurHp -= Stat_OnChangeCurHp;
        stat.OnChangeCurHp += Stat_OnChangeCurHp;
        Stat_OnChangeCurHp(stat.CurHp);

        stat.OnChangeAttackPower += Stat_OnChangeAttackPower;
        stat.OnChangeAttackPower += Stat_OnChangeAttackPower;
        Stat_OnChangeAttackPower(stat.AttackPower);
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

    [ContextMenu("levelup")]
    private void TempLevelUp()
    {
        playableStat.SetPerStat(PerStat.MaxHp, 10);
        playableStat.SetFixedStat(FixedStat.MaxHp, 10);
    }
}
