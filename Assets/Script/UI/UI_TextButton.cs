using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class UI_TextButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action ClickEvent;

    [SerializeField] TextValues pointEnter;
    [SerializeField] TextValues pointExit;

    private TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ClickEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.DOColor(pointEnter.color, pointEnter.colorDuration);
        transform.DOScale(pointEnter.size, pointEnter.sizeDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.DOColor(pointExit.color, pointExit.colorDuration);
        transform.DOScale(pointExit.size, pointExit.sizeDuration);
    }
}
[Serializable]
public class TextValues
{
    public Color color = Color.white;
    public float colorDuration;
    public Vector3 size;
    public float sizeDuration;
}