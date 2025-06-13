using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a dynamic deck of cards for a player or enemy.
/// Supports drawing and shuffling cards.
/// </summary>
public class Deck : MonoBehaviour
{
    [Tooltip("Cards currently in the deck.")]
    public List<Card> cards = new List<Card>();

    /// <summary>
    /// Draws and removes the top card from the deck.
    /// Returns a cloned copy based on its specific type.
    /// </summary>
    public Card DrawCard()
    {
        if (cards.Count == 0) return null;

        Card original = cards[0];
        cards.RemoveAt(0);

        // Clone by type to avoid using shared references
        if (original is HeroCard hero)
            return hero.Clone();

        if (original is PlayCard play)
            return play.Clone();

        return null;
    }

    /// <summary>
    /// Shuffles the deck in-place using a Fisher-Yates-style shuffle.
    /// </summary>
    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int rand = Random.Range(i, cards.Count);
            (cards[i], cards[rand]) = (cards[rand], cards[i]); // Tuple swap
        }
    }
}