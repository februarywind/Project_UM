using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] UI_TextButton continueButton;
    [SerializeField] UI_TextButton settingButton;

    [SerializeField] GameObject settingPanel;

    private void Awake()
    {
        continueButton.ClickEvent += ContinueButton_ClickEvent;
        settingButton.ClickEvent += SettingButton_ClickEvent;
    }

    private void SettingButton_ClickEvent()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    private void ContinueButton_ClickEvent()
    {
        SceneLoadManager.Instance.MoveScene(SceneName.Vilige);
    }
}
