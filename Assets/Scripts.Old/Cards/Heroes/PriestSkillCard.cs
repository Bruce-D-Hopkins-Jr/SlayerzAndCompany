using UnityEngine;

[CreateAssetMenu(fileName = "PriestSkillCard", menuName = "Cards/SkillCard/Priest")]
public class PriestSkillCard : SkillCard
{
    [SerializeField] private PriestSkill priestSkill;

    public PriestSkill PriestSkill => priestSkill;
}

public enum PriestSkill
{
    HolyLight,
    BlessingOfVigor,
    Prayer,
    Consecration,
    DivineGrace,
    DivineIntervention
}
