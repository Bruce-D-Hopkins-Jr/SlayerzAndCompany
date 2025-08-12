using UnityEngine;

public class SlayManager : MonoBehaviour
{
    private HeroCombatController selectedHero;

    public static SlayManager Instance;

    private void OnEnable()
    {
        PhaseManagerEvents.OnSlayPhaseStarted += ResetSlayState;
    }

    private void OnDisable()
    {
        PhaseManagerEvents.OnSlayPhaseStarted -= ResetSlayState;
    }

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
        if (PhaseManager.Instance.CurrentPhase != GamePhase.SLAY || selectedHero == null) return;

        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var monster = hit.collider.GetComponent<MonsterCombatController>();                
                if (monster != null)
                {
                    Debug.Log($"[SlayManager] Clicked on monster: {monster.MonsterData.MonsterName}");

                    selectedHero.TryAttack(monster);
                    selectedHero = null; // Deselect hero after attack
                }
            }
        }
    }

    public void SelectHero(HeroCombatController hero)
    {
        if (PhaseManager.Instance.CurrentPhase != GamePhase.SLAY)
        {
            Debug.LogWarning("[SlayManager] Can't select hero — not in SLAY phase.");
            return;
        }

        if (hero == null)
        {
            Debug.LogWarning("[SlayManager] Tried to select null hero.");
            return;
        }

        if (selectedHero != null)
        {
            var previousVisual = selectedHero.GetComponent<HeroVisual>();
            if (selectedHero.HasActed)
                previousVisual?.SetRingState(HeroRingState.Used);
            else
                previousVisual?.SetRingState(HeroRingState.Available);
        }

        selectedHero = hero;

        if (selectedHero.HasActed)
        {
            Debug.Log("This hero has already acted this turn");
        }
        else
        {            
            selectedHero.GetComponent<HeroVisual>()?.SetRingState(HeroRingState.Selected);

            if (EncounterManager.Instance.GetActiveEncounterMonsters().Count != 0)
            {
                foreach (var monster in EncounterManager.Instance.GetActiveEncounterMonsters())
                {
                    var visual = monster.GetComponent<MonsterVisual>();
                    visual?.ShowTargetIndicator(true);
                }
            }            
        }        

        Debug.Log($"[SlayManager] Selected hero: {hero.HeroData.HeroType}");
    }

    private void ResetSlayState()
    {
        selectedHero = null;
    }

    public void CheckForSlayPhaseEnd()
    {
        var allHeroes = FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None);
        foreach (var hero in allHeroes)
        {
            if (hero.IsAlive && !hero.HasActed)
                return; // Someone can still act
        }

        Debug.Log("[SlayManager] All heroes have acted. Ending SLAY phase...");
        PhaseManager.Instance.SetCurrentPhase(GamePhase.MONSTER); // Move to MONSTER phase
        PhaseManager.Instance.AdvancePhase();
    }
}
