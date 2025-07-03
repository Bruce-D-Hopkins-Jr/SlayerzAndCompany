using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    private List<HeroCombatController> heroes;

    private void OnEnable()
    {
        PhaseManagerEvents.OnMonsterPhaseStarted += HandleMonsterPhase;
    }

    private void OnDisable()
    {
        PhaseManagerEvents.OnMonsterPhaseStarted -= HandleMonsterPhase;
    }

    private void HandleMonsterPhase()
    {
        heroes = new List<HeroCombatController>(FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None));
        StartCoroutine(ExecuteMonsterAttacks());
    }

    private IEnumerator ExecuteMonsterAttacks()
    {
        List<MonsterCombatController> monsters = EncounterManager.Instance.GetActiveEncounterMonsters();

        Debug.Log($"[MonsterManager] MONSTER phase started with {monsters.Count} monsters.");

        foreach (var monster in monsters)
        {
            if (!monster.IsAlive) continue;

            var target = GetRandomAliveHero();
            if (target == null)
            {
                Debug.Log("[MonsterManager] No heroes left to attack.");
                yield break;
            }

            Debug.Log($"[MonsterManager] {monster.MonsterData.MonsterName} attacks {target.HeroData.HeroType} for {monster.MonsterData.Attack} damage!");
            target.TakeDamage(monster.MonsterData.Attack);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);
        //Temp
        PhaseManager.Instance.SetCurrentPhase(GamePhase.SLAY);
        PhaseManager.Instance.AdvancePhase();
    }

    private HeroCombatController GetRandomAliveHero()
    {
        List<HeroCombatController> alive = heroes.FindAll(h => h.IsAlive);
        return alive.Count > 0 ? alive[Random.Range(0, alive.Count)] : null;
    }
}
