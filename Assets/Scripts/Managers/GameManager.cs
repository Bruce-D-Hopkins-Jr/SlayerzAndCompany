using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GamePhase
{
    DRAW,
    PLAY,
    SLAY,
    MONSTER
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Hero> draftedHeroes = new();
    public BossMonster selectedBounty;
    public GamePhase currentPhase = GamePhase.DRAW;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void StartDrawPhase()
    {
        Debug.Log("Starting DRAW phase.");
        // Later: draw until 5 cards in hand
    }

    private void StartPlayPhase()
    {
        Debug.Log("Starting PLAY phase.");
        // Later: enable hand interaction
    }

    private void StartSlayPhase()
    {
        Debug.Log("Starting SLAY phase.");
        // Later: allow player to attack with heroes
    }

    private void StartMonsterPhase()
    {
        Debug.Log("Starting MONSTER phase.");
        // Later: apply monster attack and effects
    }

    public void AdvancePhase()
    {
        switch (currentPhase)
        {
            case GamePhase.DRAW:
                StartDrawPhase();
                currentPhase = GamePhase.PLAY;
                break;
            case GamePhase.PLAY:
                StartPlayPhase();
                currentPhase = GamePhase.SLAY;
                break;
            case GamePhase.SLAY:
                StartSlayPhase();
                currentPhase = GamePhase.MONSTER;
                break;
            case GamePhase.MONSTER:
                StartMonsterPhase();
                currentPhase = GamePhase.DRAW;
                break;
        }
    }

    public void StoreDraftedHeroes(List<Hero> heroes)
    {
        draftedHeroes = new List<Hero>(heroes);
    }

    public void StoreBountySelection(BossMonster bounty)
    {
        selectedBounty = bounty;
    }

    public void LoadScene()
    {
        Debug.Log("Loading dungeon...");
        SceneManager.LoadScene("TestScene");
    }
    
}
