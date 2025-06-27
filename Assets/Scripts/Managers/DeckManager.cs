using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<Card> finalDeck = new List<Card>();
    public List<UtilityCard> baseDeckCards;

    public void BuildDeck(List<Hero> selectedHeroes)
    {
        finalDeck.Clear();

        foreach(var card in baseDeckCards)
        {
            finalDeck.Add(card);
        }
        
        foreach(var hero in selectedHeroes)
        {
            foreach(var skill in hero.skillCards)
            {
                finalDeck.Add(skill);
            }
        }

        Shuffle(finalDeck);
        Debug.Log($"Deck has been build");
    }

    private void Shuffle(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            var temp = deck[i];
            int rand = Random.Range(i, deck.Count);
            deck[i] = deck[rand];
            deck[rand] = temp;
        }
    }
}
