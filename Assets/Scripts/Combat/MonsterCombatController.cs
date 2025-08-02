using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterCombatController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Monster monsterData;
    [SerializeField] private MonsterHUD hud;
    [SerializeField] private float hitDelay;
    private MonsterVisual visual;
    private int currentHP;
    private bool isAlive = true;

    public Monster MonsterData => monsterData;
    public bool IsAlive => isAlive;

    private void Awake()
    {
        visual = GetComponent<MonsterVisual>();
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"[MonsterCombatController] {monsterData.MonsterName} takes {amount} damage. HP before: {currentHP}");

        MonsterAnimator animator = GetComponent<MonsterAnimator>();
        animator?.PlayHit(hitDelay);

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

    public void OnDrop(PointerEventData eventData)
    {
        if (PhaseManager.Instance.CurrentPhase != GamePhase.PLAY) return;

        var cardUI = eventData.pointerDrag?.GetComponent<CardUI>();
        if (cardUI == null) return;

        if (!IsValidDrop(eventData))
        {
            cardUI.ReturnToHand(); // Send card back
            return;
        }

        HandManager.Instance.TryPlayCard(cardUI, this);
    }

    private bool IsValidDrop(PointerEventData eventData)
    {
        var cardUI = eventData.pointerDrag?.GetComponent<CardUI>();
        if (cardUI == null) return false;

        var card = cardUI.GetCard();

        if (card is UtilityCard uc)
        {
            return (uc.TargetType == UtilityTargetType.Monster && this is MonsterCombatController);
        }

        if (card is SkillCard sc)
        {
            return (sc.TargetType == SkillTargetType.Monster && this is MonsterCombatController);
        }

        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsValidDrop(eventData) && PhaseManager.Instance.CurrentPhase == GamePhase.PLAY)
            visual?.ShowTargetIndicator(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PhaseManager.Instance.CurrentPhase == GamePhase.PLAY)
            visual?.ShowTargetIndicator(false);
    }
}
