using UnityEngine;

[CreateAssetMenu(menuName = "Cards/HeroCard")]
public class HeroCard : Card
{
    public GameObject heroPrefab;
    public Transform heroPosition;
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
    

    public void DestroyHero()
    {

    }
}