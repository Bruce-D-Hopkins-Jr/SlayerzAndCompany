using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles UI and drag-drop interaction for a hero card.
/// </summary>
public class HeroCardUI : CardUI
{
    [Header("UI References")]
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ATKText;

    private HeroCard heroCardData;

    /// <summary>
    /// Initializes the UI elements for a hero card.
    /// </summary>
    public void Setup(Card card)
    {
        cardType.text = card.cardType.ToString();
        cardName.text = card.cardName;

        if (card is HeroCard hero)
        {
            HPText.text = hero.currentHitPoints.ToString();
            ATKText.text = hero.currentAttackPoints.ToString();
            heroCardData = hero;
        }
    }

    /// <summary>
    /// Handles drop logic by raycasting into the world to detect valid targets.
    /// </summary>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("HeroPosition"))
            {
                DropTarget dropZone = hit.collider.GetComponent<DropTarget>();
                if (dropZone != null)
                {
                    dropZone.ReceiveHeroDrop(heroCardData);
                    Destroy(gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Returns the HeroCard data associated with this UI instance.
    /// </summary>
    public HeroCard GetHeroCardData()
    {
        return heroCardData;
    }
}