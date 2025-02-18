using UnityEngine;

public class UIInventoryView : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;

    public void SetPanel(bool isOpen)
    {
        inventoryPanel.SetActive(isOpen);
    }
}
