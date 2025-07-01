using UnityEngine;

public abstract class Card : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private Sprite art;
    [SerializeField] private CardType cardType;

    public string CardName => cardName;
    public Sprite Art => art;
    public CardType CardType => cardType;
}

public enum CardType
{
    UTILITY,
    SKILL
}
