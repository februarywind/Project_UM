using UnityEngine;

public class UIEquipmentView : MonoBehaviour
{
    [SerializeField] GameObject equipmentPanel;

    public void SetPanel(bool isOpen)
    {
        equipmentPanel.SetActive(isOpen);
    }
}
