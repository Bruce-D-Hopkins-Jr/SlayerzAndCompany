using UnityEngine;

public class HeroCombatController : MonoBehaviour
{
    [SerializeField] private Hero heroData;
    private int currentHP;
    private bool hasActed;
    private bool isAlive = true;

    public Hero HeroData => heroData;
    public bool IsAlive => isAlive;
    private void OnEnable()
    {
        PhaseManagerEvents.OnSlayPhaseStarted += ResetHeroAction;
    }

    private void OnDisable()
    {
        PhaseManagerEvents.OnSlayPhaseStarted -= ResetHeroAction;
    }

    public void TryAttack(MonsterCombatController target)
    {
        Debug.Log($"[HeroCombatController] {heroData.HeroType} is trying to attack {target.MonsterData.MonsterName}");

        if (hasActed)
        {
            Debug.Log($"{heroData.HeroType} has already acted this turn.");
            return;
        }

        if (PhaseManager.Instance.CurrentPhase != GamePhase.SLAY)
        {
            Debug.Log("Not in SLAY phase.");
            return;
        }

        target.TakeDamage(heroData.Attack);
        Debug.Log($"[HeroCombatController] {heroData.HeroType} attacked for {heroData.Attack} damage!");

        hasActed = true;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"[HeroCombatController] {heroData.HeroType} takes {amount} damage. HP before: {currentHP}");

        currentHP -= amount;
        Debug.Log($"[HeroCombatController] HP after: {currentHP}");

        if (currentHP <= 0)
        {
            Die();            
        }
    }

    private void Die()
    {
        Debug.Log($"[HeroCombatController] {heroData.HeroType} has died.");
        isAlive = false;
        GameManager.Instance.NotifyHeroDefeated();
        Destroy(gameObject);
    }

    private void ResetHeroAction()
    {
        hasActed = false;
    }

    public void SetHeroData(Hero hero)
    {
        heroData = hero;
        currentHP = heroData.MaxHP;
        Debug.Log($"Set hero data for {heroData.HeroType}");
    }
}
