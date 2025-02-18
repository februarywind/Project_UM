using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;

    [SerializeField] UIStatView statView;
    public UIStatView StatView => statView;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (infoPanel.activeSelf)
            {
                infoPanel.SetActive(false);
            }
            else
            {
                infoPanel.SetActive(true);
            }
        }
    }
}
