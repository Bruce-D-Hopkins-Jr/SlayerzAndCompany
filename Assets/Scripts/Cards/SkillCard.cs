using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "Cards/SkillCard")]
public class SkillCard : Card
{
    public HeroType heroType;
    public string effect;
}
