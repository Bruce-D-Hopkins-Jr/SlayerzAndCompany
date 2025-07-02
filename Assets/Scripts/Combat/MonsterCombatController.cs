using UnityEngine;

public class MonsterCombatController : MonoBehaviour 
{
    [SerializeField] private Monster monsterData;
    private int currentHP;
    private bool isAlive = true;

    public Monster MonsterData => monsterData;
    public bool IsAlive => isAlive;

    private void Start()
    {
        Debug.Log($"[MonsterCombatController] START: {monsterData?.MonsterName ?? "null"} | currentHP = {currentHP}");
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"[MonsterCombatController] {monsterData.MonsterName} takes {amount} damage. HP before: {currentHP}");

        if (!IsAlive)
        {
            Debug.LogWarning($"[MonsterCombatController] Already dead at {currentHP} HP.");
            return;
        }

        currentHP -= amount;

        Debug.Log($"[MonsterCombatController] HP after: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
            isAlive = false;
        }
    }

    private void Die()
    {
        Debug.Log($"[MonsterCombatController] {monsterData.MonsterName} has died.");
        Destroy(gameObject);
    }

    public void SetMonsterData(Monster monster)
    {
        monsterData = monster;
        currentHP = monsterData.MaxHP;
        Debug.Log($"Set monster data for {monsterData.MonsterName}");
    }
}
