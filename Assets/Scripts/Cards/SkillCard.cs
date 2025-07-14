using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "Cards/SkillCard")]
public class SkillCard : Card
{
    [SerializeField] private HeroType heroType;
    [SerializeField, TextArea] private string description;

    public HeroType HeroType => heroType;
    public string Description => description;
}
