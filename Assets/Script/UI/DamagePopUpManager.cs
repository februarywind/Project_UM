using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpManager : MonoBehaviour
{
    [SerializeField] GameObject popUpPrefab;

    private Queue<DamagePopUp> popUpQueue = new();

    public static DamagePopUpManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void ShowDamagePopUp(Vector3 pos, string text, Color color)
    {
        if (popUpQueue.Count > 0)
        {
            popUpQueue.Dequeue().PopUpInitAndStart(pos, text, color);
            return;
        }
        Instantiate(popUpPrefab, pos, Quaternion.identity, transform).GetComponent<DamagePopUp>().PopUpInitAndStart(pos, text, color);
    }

    public void ReturnPool(DamagePopUp damagePopUp)
    {
        damagePopUp.gameObject.SetActive(false);
        popUpQueue.Enqueue(damagePopUp);
    }
}