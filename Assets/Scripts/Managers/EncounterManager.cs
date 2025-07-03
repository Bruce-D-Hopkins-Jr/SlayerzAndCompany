using UnityEngine;
using System.Collections.Generic;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance { get; private set; }

    [SerializeField] private List<Transform> encounters;
    private int currentEncounterIndex = 0;

    public Transform CurrentEncounterRoot => encounters[currentEncounterIndex];
    public int CurrentEncounterIndex => currentEncounterIndex;
    public int TotalEncounters => encounters.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }    

    public List<MonsterCombatController> GetActiveEncounterMonsters()
    {
        var monsters = new List<MonsterCombatController>(
            CurrentEncounterRoot.GetComponentsInChildren<MonsterCombatController>()
        );

        return monsters.FindAll(m => m.IsAlive);
    }

    public void NotifyMonsterDefeated()
    {
        if (GetActiveEncounterMonsters().Count == 0)
        {
            Debug.Log($"[EncounterManager] Encounter {CurrentEncounterIndex} is complete!");
            AdvanceEncounter();

            if (AllEncountersCleared())
            {
                Debug.Log("[EncounterManager] All encounters cleared! You win!");
                // Optionally trigger win condition here
            }

            PhaseManager.Instance.AdvancePhase();
        }
    }

    public void AdvanceEncounter()
    {
        currentEncounterIndex++;
        Debug.Log($"[EncounterManager] Advancing to encounter {currentEncounterIndex}");
    }

    public bool AllEncountersCleared()
    {
        return currentEncounterIndex >= encounters.Count;
    }

    
}
