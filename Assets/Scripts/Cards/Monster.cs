using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/MonsterCard")]
public class MonsterCard : Card
{
    public GameObject monsterPrefab;
    [HideInInspector] Transform monsterPosition;
    public int hitPoints;
    public int attackPoints;

    [HideInInspector] public int currentHitPoints;

    public void SpawnMonster()
    {
        currentHitPoints = hitPoints;
        Instantiate(monsterPrefab, GameObject.Find("MonsterPosition").transform);
        monsterPosition = GameObject.Find("MonsterPosition").transform;
        Debug.Log($"The {this.name} has spawned!");
    }
}