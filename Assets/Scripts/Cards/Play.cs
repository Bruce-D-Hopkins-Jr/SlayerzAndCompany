using UnityEngine;

/// <summary>
/// A ScriptableObject representing a spell-like play card used for healing or damage.
/// </summary>
[CreateAssetMenu(menuName = "Cards/PlayCard")]
public class PlayCard : Card
{
    [Header("Effect Settings")]
    public PlayCardType effectType;
    public int minEffectValue;
    public int maxEffectValue;

    [HideInInspector] public int currentEffectValue;

    /// <summary>
    /// Initializes the card's effect value within the configured min/max range.
    /// </summary>
    public void InitializeValue()
    {
        currentEffectValue = Random.Range(minEffectValue, maxEffectValue + 1);
    }

    /// <summary>
    /// Creates a clone of this play card and randomizes its effect value.
    /// </summary>
    public PlayCard Clone()
    {
        PlayCard clone = CreateInstance<PlayCard>();
        clone.cardName = cardName;
        clone.cardType = cardType;
        clone.effectType = effectType;
        clone.minEffectValue = minEffectValue;
        clone.maxEffectValue = maxEffectValue;

        clone.InitializeValue();

        return clone;
    }
}

/// <summary>
/// Defines the type of effect a play card can apply.
/// </summary>
public enum PlayCardType
{
    HEAL,
    DAMAGE
}