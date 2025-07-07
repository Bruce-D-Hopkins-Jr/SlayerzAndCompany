using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HeroCard", menuName = "Scriptable Objects/HeroCard")]
public class Hero : ScriptableObject
{
    [Header("Hero Info")]
    [SerializeField] private HeroType heroType;
    [SerializeField] private string trait;

    [Header("Hero Stats")]
    [SerializeField] private int maxHP;
    [SerializeField] private int minAttack;
    [SerializeField] private int maxAttack;

    [Header("Visuals")]
    [SerializeField] private Sprite portrait;
    [SerializeField] private GameObject heroPrefab;

    [Header("Skill Cards")]
    [SerializeField] private List<SkillCard> skillCards;

    public HeroType HeroType => heroType;
    public string Trait => trait;
    public int MaxHP => maxHP;
    public int Attack => Random.Range(minAttack, maxAttack + 1);
    public Sprite Portrait => portrait;
    public GameObject HeroPrefab => heroPrefab;
    public List<SkillCard> SkillCards => skillCards;
}

public enum HeroType
{
    Warrior,
    Scout,
    Mage,
    Priest
}