using UnityEngine;
using System;
using System.Collections.Generic;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance { get; private set; }

    [SerializeField] private List<Transform> encounters;     // encounter parents, in order
    [SerializeField] private Transform heroes;               // party root
    [SerializeField] private List<Transform> heroPositions;  // matching travel anchors (optional if same as encounters)
    [SerializeField] private float moveSpeed = 5f;

    public event Action OnTravelStarted;
    public event Action OnTravelCompleted;

    public int CurrentEncounterIndex { get; private set; } = 0;
    public int TotalEncounters => encounters.Count;

    public bool IsMoving { get; private set; } = false;

    private Vector3 targetPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (!IsMoving) return;

        heroes.position = Vector3.MoveTowards(heroes.position, targetPosition, moveSpeed * Time.deltaTime);

        if ((heroes.position - targetPosition).sqrMagnitude < 0.0001f)
        {
            heroes.position = targetPosition;
            IsMoving = false;
            Debug.Log("[EncounterManager] HeroContainer arrived at new encounter position.");

            // stop run anims
            foreach (HeroAnimator animator in heroes.GetComponentsInChildren<HeroAnimator>())
                animator?.PlayRun(false);

            OnTravelCompleted?.Invoke();
        }
    }

    // --- Query API ---

    public Transform CurrentEncounterRoot
        => (CurrentEncounterIndex >= 0 && CurrentEncounterIndex < encounters.Count) ? encounters[CurrentEncounterIndex] : null;

    public bool HasNextEncounter() => CurrentEncounterIndex < encounters.Count - 1;

    public bool IsEncounterCleared()
    {
        var root = CurrentEncounterRoot;
        if (root == null) return true;

        var mons = root.GetComponentsInChildren<MonsterCombatController>(true);
        foreach (var m in mons)
            if (m != null && m.IsAlive) return false;

        return true;
    }

    public List<MonsterCombatController> GetActiveEncounterMonsters()
    {
        var list = new List<MonsterCombatController>();
        var root = CurrentEncounterRoot;
        if (root == null) return list;

        foreach (var m in root.GetComponentsInChildren<MonsterCombatController>(true))
            if (m.IsAlive) list.Add(m);

        return list;
    }

    // --- Travel API ---

    /// <summary>
    /// PhaseManager calls this when leaving SLAY and there IS a next encounter.
    /// Kicks off hero movement; when done, OnTravelCompleted fires so PhaseManager can enter MONSTER.
    /// </summary>
    public void BeginTravelToNextEncounter()
    {
        if (!HasNextEncounter())
        {
            Debug.LogWarning("[EncounterManager] BeginTravelToNextEncounter called but no next encounter exists.");
            // Fail-safe to not deadlock phases; let PM handle victory path.
            OnTravelCompleted?.Invoke();
            return;
        }

        CurrentEncounterIndex++;

        // Choose the next target point: heroPositions (if supplied) or encounter root position
        var anchor = (CurrentEncounterIndex < heroPositions.Count && heroPositions[CurrentEncounterIndex] != null)
            ? heroPositions[CurrentEncounterIndex]
            : encounters[CurrentEncounterIndex];

        targetPosition = anchor.position;
        IsMoving = true;

        Debug.Log($"[EncounterManager] HeroContainer will move to {anchor.name}");

        foreach (HeroAnimator animator in heroes.GetComponentsInChildren<HeroAnimator>())
            animator?.PlayRun(true);

        OnTravelStarted?.Invoke();
    }

    // --- Removed/Changed methods ---

    // OLD: NotifyMonsterDefeated() did phase/UI work and delayed MoveHeroes via Invoke.
    // NEW: Let PhaseManager own phase transitions & victory. Call IsEncounterCleared()
    // from wherever you detect monster death; if true, PhaseManager decides TRAVEL or VICTORY.

    // OLD: AdvanceEncounter() manually bumped index.
    // NEW: index is bumped inside BeginTravelToNextEncounter() to keep travel logic atomic.
}

