using UnityEngine;

/// <summary>
/// A ScriptableObject representing a monster enemy, including prefab, stats, and spawn logic.
/// </summary>
[CreateAssetMenu(menuName = "Cards/MonsterCard")]
public class MonsterCard : Card
{
    [Header("Monster Setup")]
    public GameObject monsterPrefab;

    [Header("Stats")]
    public int hitPoints;
    public int attackPoints;

    [HideInInspector] public int currentHitPoints;

    private Transform monsterPosition;

    /// <summary>
    /// Spawns the monster prefab at the MonsterPosition anchor in the scene.
    /// </summary>
    public void SpawnMonster()
    {
        currentHitPoints = hitPoints;

        monsterPosition = GameObject.Find("MonsterPosition")?.transform;
        if (monsterPosition == null)
        {
            Debug.LogError("No GameObject named 'MonsterPosition' found in the scene.");
            return;
        }

        Instantiate(monsterPrefab, monsterPosition);
        Debug.Log($"The {name} has spawned!");
    }
}