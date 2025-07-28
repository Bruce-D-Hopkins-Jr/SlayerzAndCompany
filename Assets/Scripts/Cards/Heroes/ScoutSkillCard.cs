using UnityEngine;

[CreateAssetMenu(fileName = "ScoutSkillCard", menuName = "Cards/SkillCard/Scout")]
public class ScoutSkillCard : SkillCard
{
    [SerializeField] private ScoutSkill scoutSkill;

    public ScoutSkill ScoutSkill => scoutSkill;
}

public enum ScoutSkill
{
    QuickSlash,
    LightStep,
    ReconPatch,
    Ambush,
    PartyRations,
    Backstab
}
