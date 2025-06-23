using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroCard", menuName = "Scriptable Objects/HeroCard")]
public class HeroCard : ScriptableObject
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
}

public enum HeroType
{
    Warrior,
    Scout,
    Mage,
    Priest
}