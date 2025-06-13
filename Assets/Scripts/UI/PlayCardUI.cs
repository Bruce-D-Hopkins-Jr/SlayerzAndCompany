using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles UI setup and drag-and-drop logic for play cards (e.g., healing or damage).
/// </summary>
public class PlayCardUI : CardUI
{
    [Header("UI References")]
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI effectText;

    private PlayCard playCardData;

    /// <summary>
    /// Initializes the UI elements of the play card.
    /// </summary>
    public void Setup(Card card)
    {
        cardType.text = card.cardType.ToString();
        cardName.text = card.cardName;

        if (card is PlayCard play)
        {
            effectText.text = play.currentEffectValue.ToString();
            playCardData = play;
        }
    }

    /// <summary>
    /// Handles drop logic by raycasting into the world and sending the card to a valid target.
    /// </summary>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        DropTarget dropZone = hit.collider.GetComponent<DropTarget>();
        if (dropZone == null) return;

        if (hit.collider.CompareTag("MonsterPosition"))
        {
            dropZone.ReceiveMonsterDrop(playCardData);
        }
        else if (hit.collider.CompareTag("HeroPosition"))
        {
            dropZone.ReceiveHeroDrop(playCardData);
        }

        Destroy(gameObject); // Remove UI card from hand
    }

    /// <summary>
    /// Returns the associated play card data.
    /// </summary>
    public PlayCard GetPlayCardData()
    {
        return playCardData;
    }
}
