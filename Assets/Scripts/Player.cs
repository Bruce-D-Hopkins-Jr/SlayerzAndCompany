using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a player in the game, managing their hand, board state, and hero actions.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public string playerName;
    public int lifePoints = 25;

    [Header("Gameplay State")]
    public List<Card> hand = new();
    public List<HeroCard> heroes = new();

    public bool playedHero = false;
    public bool playedPlayCard = false;

    [Header("Hero Placement")]
    public Transform heroPositions;

    /// <summary>
    /// Reduces the player's life points when taking damage.
    /// </summary>
    public void TakeDamage(int amount)
    {
        lifePoints -= amount;
        Debug.Log($"{playerName} takes {amount} damage! Life Points: {lifePoints}");
    }

    /// <summary>
    /// Resets the turn-specific actions like played hero and play card flags.
    /// </summary>
    public void ResetTurn()
    {
        playedHero = false;
        playedPlayCard = false;
        Debug.Log($"{playerName}'s turn has been reset.");
    }

    /// <summary>
    /// Attempts to spawn a hero in the first available hero slot.
    /// </summary>
    public void SpawnHero(HeroCard heroCard)
    {
        foreach (Transform position in heroPositions)
        {
            if (position.childCount == 0)
            {
                GameObject instance = Instantiate(heroCard.heroPrefab, position);
                heroCard.modelInstance = instance;
                Debug.Log($"Hero {heroCard.cardName} has spawned at {position.position}");
                return;
            }
        }

        Debug.Log("There are already 3 heroes on your board.");
    }

    /// <summary>
    /// Directly spawns a hero to a specified location.
    /// </summary>
    public void SpawnHero(HeroCard heroCard, Transform spawnPoint)
    {       
        GameObject instance = Instantiate(heroCard.heroPrefab, spawnPoint);
        heroCard.modelInstance = instance;

        HeroReference reference = instance.GetComponent<HeroReference>();
        if (reference != null)
        {
            reference.heroCard = heroCard;
        }

        Debug.Log($"Hero {heroCard.cardName} spawned at specified location.");
    }
}