using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public UIStatView StatView => statView;

    [SerializeField] GameObject infoPanel;

    [SerializeField] UIStatView statView;

    [SerializeField] Button statButton;

    private void Awake()
    {
        statButton.onClick.AddListener(OpenStat);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (infoPanel.activeSelf)
            {
                infoPanel.SetActive(false);
                statView.CurController.GetComponent<SkillCamera>().StatViewCamera(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                infoPanel.SetActive(true);
                statView.CurController.GetComponent<SkillCamera>().StatViewCamera(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    private void OpenStat()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}
