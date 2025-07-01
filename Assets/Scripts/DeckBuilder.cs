using System.Collections.Generic;
using UnityEngine;

public static class DeckBuilder
{
    public static List<Card> BuildDeck(List<Hero> selectedHeroes, List<UtilityCard> baseDeck)
    {
        List<Card> newDeck = new(baseDeck);

        foreach (Hero hero in selectedHeroes)
        {
            newDeck.AddRange(hero.SkillCards);
        }

        Shuffle(newDeck);
        return newDeck;
    }

    private static void Shuffle(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            (deck[i], deck[rand]) = (deck[rand], deck[i]);
        }
    }
}
