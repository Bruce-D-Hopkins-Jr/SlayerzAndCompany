using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform handContainer;
    [SerializeField] private GameObject cardUIPrefab;

    [Header("Config")]
    [SerializeField] private int maxHandSize = 5;
    [SerializeField] private List<Card> currentHand = new();

    public static HandManager Instance;

    public int HandSize => currentHand.Count;
    public List<Card> GetCurrentHand() => new(currentHand);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void DrawUntilFull()
    {
        int needed = maxHandSize - currentHand.Count;
        if (needed <= 0) return;

        List<Card> drawn = DeckManager.Instance.Draw(needed);
        DrawCards(drawn);
    }

    public void DrawCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            if (currentHand.Count >= maxHandSize) break;

            currentHand.Add(card);
            CreateCardUI(card);
        }

        Debug.Log($"[HandManager] Drew {cards.Count} cards. Total in hand: {currentHand.Count}");
    }

    public void ResetHand()
    {
        currentHand.Clear();

        foreach (Transform child in handContainer)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("[HandManager] Hand cleared.");
    }

    private void CreateCardUI(Card card)
    {
        GameObject ui = Instantiate(cardUIPrefab, handContainer);
        var uiComponent = ui.GetComponent<CardUI>();
        if (uiComponent != null)
            uiComponent.Setup(card);
    }

    public void DiscardCard(Card card)
    {
        if (!currentHand.Contains(card)) return;

        currentHand.Remove(card);
        DeckManager.Instance.AddToDiscard(card);
        RefreshHandUI();
        Debug.Log($"[HandManager] Discarded: {card.CardName}");
    }

    private void RefreshHandUI()
    {
        foreach (Transform child in handContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in currentHand)
        {
            CreateCardUI(card);
        }
    }

    public void TryPlayCard(CardUI cardUI, MonoBehaviour target)
    {
        if (PhaseManager.Instance.CurrentPhase != GamePhase.PLAY) return;

        Card card = cardUI.GetCard();

        if (!currentHand.Contains(card))
        {
            Debug.LogWarning("[HandManager] Tried to play a card not in hand.");
            return;
        }

        // 1. Remove card data from logic
        currentHand.Remove(card);
        // 2. Add it to the discard pile
        DeckManager.Instance.AddToDiscard(card);
        // 3. Activate card effect
        if (card is UtilityCard utilityCard)
        {
            UtilityCardEffectHandler.ApplyEffect(utilityCard, target);
        }
        else if (card is SkillCard skillCard)
        {
            SkillCardEffectHandler.ApplyEffect(skillCard, target);
        }
        // 4. Destroy the specific UI card that was dropped
        Destroy(cardUI.gameObject);

        if (PhaseManager.Instance.CurrentPhase == GamePhase.PLAY && currentHand.Count == 0)
        {
            PhaseManager.Instance.SetCurrentPhase(GamePhase.SLAY);
            PhaseManager.Instance.AdvancePhase(); // PLAY → SLAY
        }

        Debug.Log($"[HandManager] Played and discarded: {card.CardName}");
    }
}
