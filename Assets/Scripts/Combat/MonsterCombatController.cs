using UnityEngine;

public class MonsterCombatController : MonoBehaviour 
{
    [SerializeField] private Monster monsterData;
    [SerializeField] private MonsterHUD hud;
    private int currentHP;
    private bool isAlive = true;

    public Monster MonsterData => monsterData;
    public bool IsAlive => isAlive;

    public void TakeDamage(int amount)
    {
        Debug.Log($"[MonsterCombatController] {monsterData.MonsterName} takes {amount} damage. HP before: {currentHP}");

        currentHP -= amount;
        Debug.Log($"[MonsterCombatController] HP after: {currentHP}");

        hud = gameObject.GetComponentInChildren<MonsterHUD>();
        hud?.UpdateHealth(currentHP);

        if (currentHP <= 0)
        {
            Die();            
        }
    }

    private void Die()
    {
        Debug.Log($"[MonsterCombatController] {monsterData.MonsterName} has died.");
        isAlive = false;
        EncounterManager.Instance.NotifyMonsterDefeated();
        Destroy(gameObject);
    }

    public void SetMonsterData(Monster monster)
    {
        monsterData = monster;
        currentHP = monsterData.MaxHP;
        Debug.Log($"Set monster data for {monsterData.MonsterName}");
    }
}
