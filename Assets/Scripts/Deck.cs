using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public Card DrawCard()
    {
        if (cards.Count == 0) return null;
        Card drawn = cards[0];
        cards.RemoveAt(0);
        return drawn;
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int rand = Random.Range(i, cards.Count);
            cards[i] = cards[rand];
            cards[rand] = temp;
        }
    }
}