using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    [SerializeField] private string monsterName;
    [SerializeField] private int maxHP;
    [SerializeField] private int attack;
    [SerializeField] private Sprite portrait;
    [SerializeField] private GameObject monsterPrefab;

    public string MonsterName => monsterName;
    public int MaxHP => maxHP;
    public int Attack => attack;
    public Sprite Portrait => portrait;
    public GameObject MonsterPrefab => monsterPrefab;
}
