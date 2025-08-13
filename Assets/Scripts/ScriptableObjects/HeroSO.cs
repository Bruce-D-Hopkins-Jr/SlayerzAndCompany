using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroSO", menuName = "Scriptable Objects/Heroes")]
public class HeroSO : ScriptableObject
{
    [Header("Hero Info")]
    [SerializeField] private string heroName = "Scout";
    [SerializeField, TextArea] private string passiveDesc = "+1 draw on first turn.";

    //TODO add field for List of hero starter cards

    [Header("Stats & Passives")]
    [SerializeField] private int startingHPBonus = 0;
    [SerializeField] private int startingBlockBonus = 0;
    [SerializeField] private bool drawPlusOneOnFirstTurn = false;

    // Public read-only accessors
    public string HeroName => heroName;
    public string PassiveDesc => passiveDesc;
    public int StartingHPBonus => startingHPBonus;
    public int StartingBlockBonus => startingBlockBonus;
    public bool DrawPlusOneOnFirstTurn => drawPlusOneOnFirstTurn;
}
