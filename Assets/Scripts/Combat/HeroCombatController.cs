using UnityEngine;
using UnityEngine.EventSystems;

public class HeroCombatController : MonoBehaviour, IDropHandler
{
    [SerializeField] private Hero heroData;
    [SerializeField] private HeroHUD hud;
    private HeroVisual visual;
    private int currentHP;
    private bool hasActed;
    private bool isAlive = true;

    public Hero HeroData => heroData;
    public bool IsAlive => isAlive;
    public bool HasActed => hasActed;

    private void Awake()
    {
        visual = GetComponent<HeroVisual>();
    }

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

        HeroAnimator animator = GetComponent<HeroAnimator>();
        animator?.PlayAttack();

        target.TakeDamage(heroData.Attack);
        Debug.Log($"[HeroCombatController] {heroData.HeroType} attacked for {heroData.Attack} damage!");

        hasActed = true;
        visual?.SetRingState(HeroRingState.Used);

        SlayManager.Instance.CheckForSlayPhaseEnd();

        if (EncounterManager.Instance.GetActiveEncounterMonsters().Count > 0)
        {
            foreach (var monster in EncounterManager.Instance.GetActiveEncounterMonsters())
            {
                var visual = monster.GetComponent<MonsterVisual>();
                visual?.ShowTargetIndicator(false);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"[HeroCombatController] {heroData.HeroType} takes {amount} damage. HP before: {currentHP}");

        HeroAnimator animator = GetComponent<HeroAnimator>();
        animator?.PlayHit();

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, heroData.MaxHP);
        Debug.Log($"[HeroCombatController] HP after: {currentHP}");

        hud = gameObject.GetComponentInChildren<HeroHUD>();
        hud?.UpdateHealth(currentHP);

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

    public void OnDrop(PointerEventData eventData)
    {
        var cardUI = eventData.pointerDrag?.GetComponent<CardUI>();
        if (cardUI == null) return;

        HandManager.Instance.TryPlayCard(cardUI, this);
    }
}
