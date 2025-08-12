using UnityEngine;

public class SkillCard : Card
{
    [SerializeField] private HeroType heroType;
    [SerializeField] private SkillEffectType effectType;
    [SerializeField] private SkillTargetType targetType;
    [SerializeField] private SkillCardTier skillCardTier;
    [SerializeField] private int effectValue;
    [SerializeField, TextArea] private string description;

    public HeroType HeroType => heroType;
    public SkillEffectType EffectType => effectType;
    public SkillTargetType TargetType => targetType;
    public SkillCardTier SkillCardTier => skillCardTier;
    public int EffectValue => effectValue;    
    public string Description => description;
}
