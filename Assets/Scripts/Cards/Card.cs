using UnityEngine;

public enum CardType { HERO, MONSTER, PLAY }

public class Card : ScriptableObject
{
    public string cardName;
    public CardType cardType;
}
