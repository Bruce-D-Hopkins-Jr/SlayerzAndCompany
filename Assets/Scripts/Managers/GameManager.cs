using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Hero> draftedHeroes = new();
    public Monster selectedBounty;

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

    public void StoreDraftedHeroes(List<Hero> heroes)
    {
        draftedHeroes = new List<Hero>(heroes);
    }

    public void StoreBountySelection(Monster bounty)
    {
        selectedBounty = bounty;
    }

    public void LoadScene()
    {
        Debug.Log("Loading dungeon...");
        SceneManager.LoadScene("TestScene");
    }

    public void NotifyHeroDefeated()
    {
        var heroes = FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None);
        bool anyAlive = false;

        foreach (var hero in heroes)
        {
            if (hero.IsAlive)
            {
                anyAlive = true;
                break;
            }
        }

        if (!anyAlive)
        {
            Debug.Log("[GameManager] All heroes are dead. Game Over.");
            // Trigger defeat logic
            FindAnyObjectByType<GameOverUIManager>().ShowGameOver(false);
        }
    }
}
