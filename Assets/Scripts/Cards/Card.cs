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

public enum UtilityEffectType
{
    Heal,
    HealAOE,
    Damage,
    DamageAOE,
    DrawCards
}

public enum UtilityTargetType
{
    Hero,
    Monster,
    None // For non-targeted effects like draw
}

public enum SkillEffectType
{
    Damage,
    DamageAOE,
    Heal,
    HealAOE,
    Buff,
    BuffAOE,
    Draw
}

public enum SkillTargetType
{
    Hero,
    Monster,
    None // For non-targeted effects like draw
}

public enum SkillCardTier
{
    Bronze,
    Silver,
    Gold
}
