using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "Cards/SkillCard")]
public class SkillCard : Card
{
    [SerializeField] private HeroType heroType;
    [SerializeField, TextArea] private string effect;

    public HeroType HeroType => heroType;
    public string Effect => effect;
}
