using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> heroSpawnPoints;
    [SerializeField] private List<Transform> monsterSpawnPoints;
    [SerializeField] private List<Monster> tyrantula;
    [SerializeField] private List<Monster> hauntclaw;
    [SerializeField] private List<Monster> kingbee;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Spawn selected heroes
        SpawnHeroes();

        //Spawn Monsters based on selected boss monster
        var bountyMonster = GameManager.Instance.selectedBounty;
        if (bountyMonster == null)
        {
            Debug.LogWarning("No bounty monster found");
            return;
        }

        switch (bountyMonster.MonsterType)
        {
            case MonsterType.TYRANTULA:
                SpawnMonsters(tyrantula);
                break;
            case MonsterType.HAUNTCLAW:
                SpawnMonsters(hauntclaw);
                break;
            case MonsterType.KINGBEE:
                SpawnMonsters(kingbee);
                break;
            default:
                Debug.LogWarning($"No spawn logic for monster type: {bountyMonster.MonsterType}");
                break;
        }
    }

    private void SpawnHeroes()
    {
        var heroes = GameManager.Instance.draftedHeroes;
        if (heroes == null || heroes.Count == 0)
        {
            Debug.LogWarning("No drafted heroes found");
            return;
        }

        for (int i = 0; i < heroes.Count && i < heroSpawnPoints.Count; i++)
        {
            GameObject prefab = heroes[i].HeroPrefab;
            if (prefab != null)
            {
                GameObject instance = Instantiate(prefab, heroSpawnPoints[i]);
                HeroCombatController combat = instance.GetComponent<HeroCombatController>();
                HeroHUD hud = instance.GetComponentInChildren<HeroHUD>();

                if(combat != null && hud != null)
                {
                    string heroName = heroes[i].HeroType.ToString();
                    Sprite heroSprite = heroes[i].Portrait;
                    int heroHP = heroes[i].MaxHP;

                    combat.SetHeroData(heroes[i]);
                    hud.Setup(heroName, heroSprite, heroHP);
                    hud.UpdateHealth(heroHP);
                }
                
                Debug.Log($"Spawned Hero: {heroes[i].HeroType}");                
            }
            else
            {
                Debug.LogWarning($"No prefab assigned for hero: {heroes[i].HeroType}");
            }
        }
    }

    private void SpawnMonsters(List<Monster> monsterList)
    {
        for (int i = 0; i < monsterList.Count && i < monsterSpawnPoints.Count; i++)
        {
            GameObject prefab = monsterList[i].MonsterPrefab;
            if (prefab != null)
            {
                GameObject instance = Instantiate(prefab, monsterSpawnPoints[i]);
                MonsterCombatController combat = instance.GetComponent<MonsterCombatController>();

                if (combat != null)
                {
                    combat.SetMonsterData(monsterList[i]);
                }
                
                Debug.Log($"Spawned monster: {monsterList[i].MonsterName}");                
            }
            else
            {
                Debug.LogWarning($"No prefab assigned for monster: {monsterList[i].MonsterName}");
            }
        }
    }
}
