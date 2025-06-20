using UnityEngine;

/// <summary>
/// A ScriptableObject that defines a hero card with stats and a prefab reference.
/// </summary>
[CreateAssetMenu(menuName = "Cards/HeroCard")]
public class HeroCard : Card
{
    [Header("Visual & Prefab")]
    public GameObject heroPrefab;

    [HideInInspector]
    public GameObject modelInstance;

    [Header("Stat Ranges")]
    public int minHitPoints;
    public int maxHitPoints;
    public int minAttackPoints;
    public int maxAttackPoints;

    [Header("Current Stats")]
    [HideInInspector] public int currentHitPoints;
    [HideInInspector] public int currentAttackPoints;

    [Header("Attack Flag")]
    public bool hasAttacked = false;

    /// <summary>
    /// Randomizes hero stats within the configured min/max bounds.
    /// </summary>
    public void InitializeStats()
    {
        currentHitPoints = Random.Range(minHitPoints, maxHitPoints + 1);
        currentAttackPoints = Random.Range(minAttackPoints, maxAttackPoints + 1);
    }

    /// <summary>
    /// Clones this hero card into a new runtime instance with randomized stats.
    /// </summary>
    public HeroCard Clone()
    {
        HeroCard clone = CreateInstance<HeroCard>();

        // Copy base and stat info
        clone.cardName = cardName;
        clone.cardType = cardType;
        clone.heroPrefab = heroPrefab;
        clone.minHitPoints = minHitPoints;
        clone.maxHitPoints = maxHitPoints;
        clone.minAttackPoints = minAttackPoints;
        clone.maxAttackPoints = maxAttackPoints;

        // Generate new stats for the clone
        clone.InitializeStats();

        return clone;
    }
}