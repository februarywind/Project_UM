using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] UI_TextButton continueButton;

    private void Awake()
    {
        continueButton.ClickEvent += ContinueButton_ClickEvent;
    }

    private void ContinueButton_ClickEvent()
    {
        SceneLoadManager.Instance.MoveScene(SceneName.Vilige);
    }
}
