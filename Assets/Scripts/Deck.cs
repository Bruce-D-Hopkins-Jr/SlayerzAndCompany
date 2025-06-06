using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public Card DrawCard()
    {
        if (cards.Count == 0) return null;
        Card original = cards[0];
        cards.RemoveAt(0);

        if (original is HeroCard hero) return hero.Clone();
        if (original is PlayCard play) return play.Clone();

        return null;
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