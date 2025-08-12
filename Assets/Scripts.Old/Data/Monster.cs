using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    [SerializeField] private MonsterType monsterType;
    [SerializeField] private MonsterStage monsterStage;
    [SerializeField] private string monsterName;
    [SerializeField] private int maxHP;
    [SerializeField] private int minAttack;
    [SerializeField] private int maxAttack;
    [SerializeField] private Sprite portrait;
    [SerializeField] private GameObject monsterPrefab;

    public string MonsterName => monsterName;
    public int MaxHP => maxHP;
    public int Attack => Random.Range(minAttack, maxAttack + 1);
    public Sprite Portrait => portrait;
    public GameObject MonsterPrefab => monsterPrefab;
    public MonsterType MonsterType => monsterType;
}

public enum MonsterType
{
    TYRANTULA,
    HAUNTCLAW,
    KINGBEE
}

public enum MonsterStage
{
    FOUNDLING,
    MATURE,
    ASCENDED
}
