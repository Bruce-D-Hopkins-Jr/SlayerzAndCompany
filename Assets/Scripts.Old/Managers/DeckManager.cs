using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<UtilityCard> baseDeckCards;

    [SerializeField] private List<Card> deck = new();
    [SerializeField] private List<Card> discardPile = new();

    public static DeckManager Instance { get; private set; }   

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BuildDeck(List<Hero> selectedHeroes)
    {
        deck = DeckBuilder.BuildDeck(selectedHeroes, baseDeckCards);
        discardPile.Clear();
        Debug.Log("Deck built and shuffled.");
    }       

    public void ResetDeck()
    {
        deck.Clear();
        discardPile.Clear();
    }

    public List<Card> Draw(int count)
    {
        List<Card> drawn = new();

        for (int i = 0; i < count; i++)
        {
            if (deck.Count == 0)
            {
                Debug.Log("Deck is empty");
                break;
            }

            if (deck.Count == 0) break; // Still empty after reshuffle

            drawn.Add(deck[0]);
            deck.RemoveAt(0);
        }

        return drawn;
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

    public void ClearAndRebuildDeck()
    {
        deck.Clear();
        discardPile.Clear();

        // Rebuild from heroes you previously stored
        BuildDeck(GameManager.Instance.draftedHeroes);
    }

    public void AddToDiscard(Card card)
    {
        discardPile.Add(card);
    }

    public int CardsRemaining()
    {
        return deck.Count;
    }

}
