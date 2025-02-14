using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] PlayableStatController statController;
    [SerializeField] GameObject statCanvas;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text staminaText;
    [SerializeField] TMP_Text attackPowerText;
    private Slider hpSlider;

    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();
        hpSlider.maxValue = statController.Stat.MaxHp;

        statController.Stat.OnChangeCurHp += Stat_OnChangeCurHp;

        Stat_OnChangeCurHp(statController.Stat.CurHp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            statCanvas.SetActive(!statCanvas.activeSelf);
        }
    }

    private void Stat_OnChangeCurHp(float value)
    {
        hpSlider.value = value;
        hpText.text = $"HP : {statController.Stat.CurHp} / {statController.Stat.MaxHp}";
    }
}
