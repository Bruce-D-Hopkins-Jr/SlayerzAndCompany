using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Sprite cardArtImage;
    [SerializeField] private TextMeshProUGUI cardNameText;    
    [SerializeField] private TextMeshProUGUI cardDescriptionText;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Card card;
    private Vector3 originalScale;
    private float originalAlpha;

    public Card GetCard() => card;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void Setup(Card card)
    {
        this.card = card;
        cardArtImage = card.Art;
        cardNameText.text = card.CardName;

        switch (card)
        {
            case SkillCard skill:
                cardDescriptionText.text = skill.Description;                
                break;

            case UtilityCard utility:
                cardDescriptionText.text = utility.Description;                
                break;

            default:
                cardDescriptionText.text = "";
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform); // Move to top-level so it can drag freely

        originalScale = transform.localScale;
        originalAlpha = canvasGroup.alpha;

        transform.localScale = originalScale * 0.6f;        // Shrink
        canvasGroup.alpha = 0.3f;
        canvasGroup.blocksRaycasts = false; // So raycasts can hit drop zones
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero;

        transform.localScale = originalScale;              // Reset scale
        canvasGroup.alpha = originalAlpha;                 // Reset transparency
        canvasGroup.blocksRaycasts = true;
    }
}
