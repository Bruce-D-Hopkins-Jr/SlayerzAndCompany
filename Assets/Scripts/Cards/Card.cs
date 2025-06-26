using UnityEngine;

public abstract class Card : ScriptableObject
{
    public string cardName;
    public Sprite art;
    public CardType cardType;
}

public enum CardType
{
    UTILITY,
    SKILL
}
