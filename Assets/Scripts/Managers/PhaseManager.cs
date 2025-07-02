using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    public GamePhase CurrentPhase { get; private set; } = GamePhase.SLAY;

    private void Start()
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
                CurrentPhase = GamePhase.DRAW;
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
    }

    private void StartDrawPhase()
    {
        Debug.Log("Starting DRAW phase.");
        // Notify systems to draw until 5 cards, for example
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
        // Enable hero targeting/attacks
    }

    private void StartMonsterPhase()
    {
        Debug.Log("Starting MONSTER phase.");
        // Trigger monster AI
    }
}

public static class PhaseManagerEvents
{
    public static System.Action OnSlayPhaseStarted;
}

public enum GamePhase
{
    DRAW,
    PLAY,
    SLAY,
    MONSTER
}
