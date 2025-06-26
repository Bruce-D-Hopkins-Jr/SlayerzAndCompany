using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Hero> draftedHeroes = new();
    public BossMonster selectedBounty;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional if persistent
    }

    public void StoreDraftedHeroes(List<Hero> heroes)
    {
        draftedHeroes = new List<Hero>(heroes);
    }

    public void StoreBountySelection(BossMonster bounty)
    {
        selectedBounty = bounty;
    }
}
