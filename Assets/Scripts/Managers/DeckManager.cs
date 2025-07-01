using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<UtilityCard> baseDeckCards;

    private List<Card> deck = new();
    private List<Card> discardPile = new();
    private List<Card> hand = new();
    private const int HAND_LIMIT = 5;

    public void BuildDeck(List<Hero> selectedHeroes)
    {
        deck = DeckBuilder.BuildDeck(selectedHeroes, baseDeckCards);
        discardPile.Clear();
        hand.Clear();
        Debug.Log("Deck built and shuffled.");
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (hand.Count >= HAND_LIMIT)
            {
                Debug.Log("Hand is full!");
                break;
            }

            if (deck.Count == 0)
            {
                if (discardPile.Count == 0)
                {
                    Debug.Log("No cards left to draw.");
                    break;
                }
                ReshuffleDiscardIntoDeck();
            }

            var card = deck[0];
            deck.RemoveAt(0);
            hand.Add(card);
        }

        Debug.Log($"Hand has {hand.Count} cards.");
    }

    public void DiscardCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            discardPile.Add(card);
            Debug.Log($"{card.name} discarded.");
        }
    }

    private void ReshuffleDiscardIntoDeck()
    {
        Debug.Log("Reshuffling discard pile into deck...");
        deck.AddRange(discardPile);
        discardPile.Clear();

        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            (deck[i], deck[rand]) = (deck[rand], deck[i]);
        }
    }

    public void ResetDeck()
    {
        deck.Clear();
        discardPile.Clear();
        hand.Clear();
    }

    public List<Card> GetHand() => new(hand);
}
