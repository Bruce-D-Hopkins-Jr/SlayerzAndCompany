using UnityEngine;

/// <summary>
/// Base class for all cards in the game. Stores shared data for display and logic.
/// </summary>
[CreateAssetMenu(fileName = "NewCard", menuName = "Slayerz/Card")]
public class Card : ScriptableObject
{
    [Header("Card Info")]
    public string cardName;

    [TextArea]
    public string cardDescription;

    public Sprite cardArt;

    public CardType cardType;
}

/// <summary>
/// Enum to distinguish between different types of cards.
/// </summary>
public enum CardType
{
    HERO,
    PLAY,
    MONSTER
}