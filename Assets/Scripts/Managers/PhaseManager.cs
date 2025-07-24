using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    public GamePhase CurrentPhase { get; private set; } = GamePhase.DRAW;

    private void Start()
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
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentPhase == GamePhase.SLAY)
            {
                Debug.Log("[Test] Forcing advance to MONSTER phase.");
                StartMonsterPhase();
                CurrentPhase = GamePhase.MONSTER;
            }
            else if (CurrentPhase == GamePhase.MONSTER)
            {
                Debug.Log("[Test] Forcing advance to SLAY phase.");
                StartSlayPhase();
                CurrentPhase = GamePhase.SLAY;
            }
        }
        */
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TestScene")
        {
            Debug.Log("PhaseManager: TestScene loaded, beginning turn cycle.");
            AdvancePhase();
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
            case GamePhase.MONSTER:
                StartMonsterPhase();
                CurrentPhase = GamePhase.MONSTER;
                break;
        }

        PhaseUI ui = FindAnyObjectByType<PhaseUI>();
        if (ui != null)
        {
            ui.UpdateUI(CurrentPhase);
        }
    }

    private void StartDrawPhase()
    {
        Debug.Log("Starting DRAW phase.");
        // Notify systems to draw until 5 cards, for example

        HandManager handManager = FindAnyObjectByType<HandManager>();
        if (handManager == null)
        {
            Debug.LogError("HandManager not found in scene.");
            return;
        }

        // Draw cards until hand is full
        handManager.DrawUntilFull();
    }

    private void StartPlayPhase()
    {
        Debug.Log("Starting PLAY phase.");
        // Notify UI to enable card play
    }

    private void StartSlayPhase()
    {
        Debug.Log("Starting SLAY phase.");
        PhaseManagerEvents.OnSlayPhaseStarted?.Invoke();
        //Show selection rings on heroes
        foreach (var hero in FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None))
        { 
            var visual = hero.GetComponent<HeroVisual>();
            visual?.ShowRing(true);
            visual?.SetRingState(HeroRingState.Available);
        }
        // Enable hero targeting/attacks
    }

    private void StartMonsterPhase()
    {
        //Remove selection rings on heroes
        foreach (var hero in FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None))
        {
            hero.GetComponent<HeroVisual>()?.ShowRing(false);
        }
        Debug.Log("Starting MONSTER phase.");
        PhaseManagerEvents.OnMonsterPhaseStarted?.Invoke();
        // Trigger monster AI
    }

    //Temporary
    public void SetCurrentPhase(GamePhase phase)
    {
        CurrentPhase = phase;
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
    MONSTER
}
