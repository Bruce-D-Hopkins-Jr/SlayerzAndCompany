using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Base UI behavior for draggable cards.
/// Handles drag visuals and raycast behavior.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        // Fade card to indicate it's being dragged
        canvasGroup.alpha = 0.6f;

        // Allow UI raycasts to pass through while dragging
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        // Move the card relative to the canvas scale
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // Restore visuals and raycast blocking after drag ends
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
