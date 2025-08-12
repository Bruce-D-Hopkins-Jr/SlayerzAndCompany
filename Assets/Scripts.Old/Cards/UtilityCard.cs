using UnityEngine;

[CreateAssetMenu(fileName = "UtilityCard", menuName = "Cards/UtilityCard")]
public class UtilityCard : Card
{
    [SerializeField] private UtilityEffectType effectType;
    [SerializeField] private UtilityTargetType targetType;
    [SerializeField] private int effectValue;
    [SerializeField, TextArea] private string description;

    public UtilityEffectType EffectType => effectType;
    public UtilityTargetType TargetType => targetType;
    public int EffectValue => effectValue;
    public string Description => description;
}