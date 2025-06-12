using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetCanvasScaleFactor();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        canvasGroup.blocksRaycasts = true;
    }

    float GetCanvasScaleFactor()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        return canvas != null ? canvas.scaleFactor : 1f;
    }
}
