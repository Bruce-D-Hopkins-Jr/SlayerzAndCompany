using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HeroCard", menuName = "Scriptable Objects/HeroCard")]
public class Hero : ScriptableObject
{
    [Header("Hero Information")]
    public string heroName;
    public HeroType heroType;
    public string trait;

    [Header("Hero Stats")]
    public int maxHP;
    public int attack;

    [Header("Hero Visuals")]
    public Sprite portrait;

    [Header("Hero Skill Cards")]
    public List<SkillCard> skillCards;
}

public enum HeroType
{
    Warrior,
    Scout,
    Mage,
    Priest
}