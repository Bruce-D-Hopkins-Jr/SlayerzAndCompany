using UnityEngine;

[CreateAssetMenu(menuName = "Cards/HeroCard")]
public class HeroCard : Card
{
    public GameObject heroPrefab;
    [HideInInspector] public GameObject modelInstance;
    public int minHitPoints;
    public int maxHitPoints;
    public int minAttackPoints;
    public int maxAttackPoints;

    [HideInInspector] public int currentHitPoints;
    [HideInInspector] public int currentAttackPoints;

    public void InitializeStats()
    {
        currentHitPoints = Random.Range(minHitPoints, maxHitPoints + 1);
        currentAttackPoints = Random.Range(minAttackPoints, maxAttackPoints + 1);
    }
    
    public HeroCard Clone()
    {
        HeroCard clone = ScriptableObject.CreateInstance<HeroCard>();
        clone.cardName = this.cardName;
        clone.cardType = this.cardType;
        clone.heroPrefab = this.heroPrefab;
        clone.modelInstance = this.modelInstance;
        clone.minHitPoints = this.minHitPoints;
        clone.maxHitPoints = this.maxHitPoints;
        clone.minAttackPoints = this.minAttackPoints;
        clone.maxAttackPoints = this.maxAttackPoints;
        clone.currentHitPoints = this.currentHitPoints;
        clone.currentAttackPoints = this.currentAttackPoints;

        clone.InitializeStats();

        return clone;

    }
}