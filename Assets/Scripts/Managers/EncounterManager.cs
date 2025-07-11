using UnityEngine;
using System.Collections.Generic;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance { get; private set; }

    [SerializeField] private List<Transform> encounters;
    [SerializeField] private Transform heroes;
    [SerializeField] private List<Transform> heroPositions;
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
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

    private void Update()
    {
        if (isMoving)
        {
            heroes.position = Vector3.MoveTowards (heroes.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(heroes.position, targetPosition) < 0.01f)
            {
                heroes.position = targetPosition;
                isMoving = false;
                Debug.Log("[EncounterManager] HeroContainer arrived at new encounter position.");

                var animators = heroes.GetComponentsInChildren<HeroAnimator>();
                foreach (HeroAnimator animator in animators)
                {
                    if (animator != null)
                    {
                        animator.PlayRun(isMoving);
                    }
                }
            }
        }
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
                FindAnyObjectByType<GameOverUIManager>().ShowGameOver(true);
            }
            else
            {
                Invoke(nameof(MoveHeroes), 2f);
            }            
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

    public void MoveHeroes()
    {
        if (currentEncounterIndex < heroPositions.Count)
        {
            targetPosition = heroPositions[currentEncounterIndex].position;
            isMoving = true;
            Debug.Log($"[EncounterManager] HeroContainer will move to {heroPositions[currentEncounterIndex].name}");

            var animators = heroes.GetComponentsInChildren<HeroAnimator>();
            foreach(HeroAnimator animator in animators)
            {
                if (animator != null)
                {
                    animator.PlayRun(isMoving);
                }
            }
        }
        else
        {
            Debug.LogWarning("[EncounterManager] No hero position defined for this encounter.");
        }
    }

    
}
