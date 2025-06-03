using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int lifePoints = 25;

    public List<Card> hand = new();
    public List<HeroCard> heroes = new();

    public bool playedHero = false;
    public bool playedPlayCard = false;

    public void TakeDamage(int amount)
    {
        lifePoints -= amount;
        Debug.Log($"{playerName} takes {amount} damage! Life Points: {lifePoints}");
    }

    public void ResetTurn()
    {
        playedHero = false;
        playedPlayCard = false;
        Debug.Log($"{playerName}'s turn has been reset.");
    }
}