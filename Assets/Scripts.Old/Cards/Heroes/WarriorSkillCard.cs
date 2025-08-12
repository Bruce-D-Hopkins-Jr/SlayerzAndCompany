using UnityEngine;

[CreateAssetMenu(fileName = "WarriorSkillCard", menuName = "Cards/SkillCard/Warrior")]
public class WarriorSkillCard : SkillCard
{
    [SerializeField] private WarriorSkill warriorSkill;

    public WarriorSkill WarriorSkill => warriorSkill;
}

public enum WarriorSkill
{
    ShoulderBash,
    SecondWind,
    BattleInstinct,
    PowerStrike,
    Whirlwind,
    Earthshatter
}