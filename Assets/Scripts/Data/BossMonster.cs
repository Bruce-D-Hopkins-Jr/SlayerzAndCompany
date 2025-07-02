using UnityEngine;

[CreateAssetMenu(fileName = "BossMonster", menuName = "Scriptable Objects/BossMonster")]
public class BossMonster : Monster
{
    [SerializeField] private MonsterType monsterType;
    [SerializeField] private string trait;

    public string Trait => trait;
    public MonsterType MonsterType => monsterType;
}

public enum MonsterType
{
    TYRANTULA,
    HAUNTCLAW,
    KINGBEE
}
