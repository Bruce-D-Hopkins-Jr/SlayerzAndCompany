using UnityEngine;

/// <summary>
/// Handles spawning card UI objects into the player's hand area.
/// </summary>
public class HandManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform handUI;

    [Header("Card Prefabs")]
    [SerializeField] private GameObject heroCard;
    [SerializeField] private GameObject playCard;

    public static HandManager Instance { get; private set; }

    private void Awake()
    {
        // Enforce singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Instantiates and sets up a card in the player's hand.
    /// </summary>
    public void AddCardToHand(Card card)
    {
        GameObject playerCard = null;

        if (card is HeroCard hero)
        {
            playerCard = Instantiate(heroCard, handUI);
            playerCard.GetComponent<HeroCardUI>()?.Setup(hero);
        }
        else if (card is PlayCard play)
        {
            playerCard = Instantiate(playCard, handUI);
            playerCard.GetComponent<PlayCardUI>()?.Setup(play);
        }

        if (playerCard == null)
        {
            Debug.LogWarning($"Failed to create a card UI for: {card.cardName}");
        }
    }
}
