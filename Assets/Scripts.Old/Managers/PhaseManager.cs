using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    public GamePhase CurrentPhase { get; private set; } = GamePhase.DRAW;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // safety: unsubscribe if still hooked
        if (EncounterManager.Instance != null)
            EncounterManager.Instance.OnTravelCompleted -= HandleTravelCompleted;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TestScene")
        {
            Debug.Log("[PhaseManager] TestScene loaded, beginning turn cycle.");
            // Start from DRAW each time entering the test scene
            SetPhase(GamePhase.DRAW);
            StartDrawPhase();
        }
    }

    public void AdvancePhase()
    {
            switch (CurrentPhase)
        {
            case GamePhase.DRAW:
                StartDrawPhase();
                //Change to GamePhase.PLAY when implementing PLAY phase
                CurrentPhase = GamePhase.PLAY;
                AdvancePhase();
                break;
            case GamePhase.PLAY:
                StartPlayPhase();
                CurrentPhase = GamePhase.PLAY;
                break;
            case GamePhase.SLAY:
                StartSlayPhase();
                CurrentPhase = GamePhase.SLAY;
                break;
            case GamePhase.TRAVEL:
                StartTravelPhase();
                CurrentPhase = GamePhase.TRAVEL;
                break;
            case GamePhase.MONSTER:
                StartMonsterPhase();
                CurrentPhase = GamePhase.MONSTER;
                break;
        }
    }

    private void StartDrawPhase()
    {
        SetPhase(GamePhase.DRAW);
        Debug.Log("Starting DRAW phase.");

        var handManager = FindAnyObjectByType<HandManager>();
        if (handManager == null)
        {
            Debug.LogError("[PhaseManager] HandManager not found in scene.");
            return;
        }

        handManager.DrawUntilFull();

        // (Auto-advance to PLAY, or keep it if you want a separate "End Draw" step)
        StartPlayPhase();
    }

    private void StartPlayPhase()
    {
        SetPhase(GamePhase.PLAY);
        Debug.Log("Starting PLAY phase.");
        // Enable card play UI/input, show Next Phase button, etc.
    }

    private void StartSlayPhase()
    {
        SetPhase(GamePhase.SLAY);
        Debug.Log("Starting SLAY phase.");

        PhaseManagerEvents.OnSlayPhaseStarted?.Invoke();

        // Show selection rings on heroes & mark available
        foreach (var hero in FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None))
        {
            var visual = hero.GetComponent<HeroVisual>();
            visual?.ShowRing(true);
            visual?.SetRingState(HeroRingState.Available);

            // Reset per-turn flags if you use them
            hero.ResetHeroAction(); // or hero.ResetAction();
        }
    }

    private void StartTravelPhase()
    {
        SetPhase(GamePhase.TRAVEL);
        Debug.Log("Starting TRAVEL phase.");

        // Ensure single subscription
        var em = EncounterManager.Instance;
        em.OnTravelCompleted -= HandleTravelCompleted;
        em.OnTravelCompleted += HandleTravelCompleted;

        em.BeginTravelToNextEncounter();
    }

    private void HandleTravelCompleted()
    {
        // Unhook immediately to avoid duplicate calls
        EncounterManager.Instance.OnTravelCompleted -= HandleTravelCompleted;
        StartMonsterPhase();
        AdvancePhase();
    }

    private void StartMonsterPhase()
    {
        // Hide selection rings
        foreach (var hero in FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None))
            hero.GetComponent<HeroVisual>()?.ShowRing(false);

        SetPhase(GamePhase.MONSTER);
        Debug.Log("Starting MONSTER phase.");
        PhaseManagerEvents.OnMonsterPhaseStarted?.Invoke();
    }

    /// <summary>
    /// Called when SLAY should end (e.g., all heroes have acted, or player pressed Next Phase).
    /// Decides TRAVEL vs MONSTER vs Victory.
    /// </summary>
    private void TryEnterTravelOrMonster()
    {
        var em = EncounterManager.Instance;
        if (em == null)
        {
            Debug.LogError("[PhaseManager] EncounterManager missing; entering MONSTER by default.");
            StartMonsterPhase();
            return;
        }

        if (em.IsEncounterCleared())
        {
            if (em.HasNextEncounter())
            {
                // Move heroes first; MONSTER for next room begins after travel completes
                StartTravelPhase();
            }
            else
            {
                // Victory: no more encounters
                Debug.Log("[PhaseManager] All encounters cleared — Victory!");
                FindAnyObjectByType<GameOverUIManager>()?.ShowGameOver(true);
            }
        }
        else
        {
            // Not cleared? Monsters in current encounter strike back now
            StartMonsterPhase();
        }
    }

    private void SetPhase(GamePhase phase)
    {
        CurrentPhase = phase;
        var ui = FindAnyObjectByType<PhaseUI>();
        ui?.UpdateUI(CurrentPhase);
    }

    // If you still need to force a phase (debug), keep this.
    public void SetCurrentPhase(GamePhase phase)
    {
        SetPhase(phase);
    }
}

public static class PhaseManagerEvents
{
    public static System.Action OnSlayPhaseStarted;
    public static System.Action OnMonsterPhaseStarted;
}

public enum GamePhase
{
    DRAW,
    PLAY,
    SLAY,
    TRAVEL,
    MONSTER
}