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
                infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }
    private void OpenStat()
    {
                infoPanel.SetActive(!infoPanel.activeSelf);
    }
}
