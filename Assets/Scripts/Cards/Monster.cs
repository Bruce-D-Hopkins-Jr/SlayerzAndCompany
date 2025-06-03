using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/MonsterCard")]
public class MonsterCard : Card
{
    public GameObject modelPrefab;
    public int hitPoints;
    public int attackPoints;

    [HideInInspector] public int currentHitPoints;

    public void SpawnMonster()
    {
        currentHitPoints = hitPoints;
        Debug.Log($"The {this.name} has spawned!");
    }
}