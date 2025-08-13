using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RunConfigSO", menuName = "Scriptable Objects/RunConfig")]
public class RunConfigSO : ScriptableObject
{
    [Header("Catalogs")]
    [SerializeField] private List<HeroSO> availableHeroes = default;

    [Header("Defaults (Vertical Slice)")]
    [Tooltip("Hand size used by GameLoop at the start of player turns.")]
    [SerializeField, Min(0)] private int startingHandSize = 5;

    // --- Read-only accessors (encapsulation) ---
    public IReadOnlyList<HeroSO> AvailableHeroes => availableHeroes;
    public int StartingHandSize => startingHandSize;
}
