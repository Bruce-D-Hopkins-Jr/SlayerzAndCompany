using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "Cards/SkillCard")]
public class SkillCard : Card
{
    [SerializeField] private HeroType heroType;
    [SerializeField] private UtilityEffectType effectType;
    [SerializeField] private UtilityTargetType targetType;
    [SerializeField] private SkillCardTier skillCardTier;
    [SerializeField] private int effectValue;
    [SerializeField, TextArea] private string description;

    public HeroType HeroType => heroType;
    public UtilityEffectType EffectType => effectType;
    public UtilityTargetType TargetType => targetType;
    public SkillCardTier SkillCardTier => skillCardTier;
    public int EffectValue => effectValue;    
    public string Description => description;
}
