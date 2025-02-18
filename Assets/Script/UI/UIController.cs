using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;

    [SerializeField] UIStatView statView;
    [SerializeField] Button statViewButton;

    [SerializeField] UIInventoryView inventoryView;
    [SerializeField] Button inventoryViewButton;

    [SerializeField] UIEquipmentView equipmentView;
    [SerializeField] Button equipmentViewButton;
    public UIStatView StatView => statView;
    public UIInventoryView InventoryView => inventoryView;
    public UIEquipmentView EquipmentView => equipmentView;
    private void Awake()
    {
        statViewButton.onClick.AddListener(() => PanelControl(1));
        inventoryViewButton.onClick.AddListener(() => PanelControl(2));
        equipmentViewButton.onClick.AddListener(() => PanelControl(3));
    }

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

    private void PanelControl(int n)
    {
        StatView.SetPanel(1 == n);
        InventoryView.SetPanel(2 == n);
        EquipmentView.SetPanel(3 == n);
    }
}
