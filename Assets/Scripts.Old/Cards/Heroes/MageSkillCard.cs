using UnityEngine;

[CreateAssetMenu(fileName = "Mage", menuName = "Cards/SkillCard/Mage")]
public class MageSkillCard : SkillCard
{    
    [SerializeField] private MageSkill mageSkill;

    public MageSkill MageSkill => mageSkill;
}

public enum MageSkill
{
    MagicBolt,
    ManaSurge,
    Focus,
    ArcaneSense,
    FlameWave,
    MeteorStrike
}