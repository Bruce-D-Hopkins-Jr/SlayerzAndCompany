using UnityEngine;

[CreateAssetMenu(fileName = "BossMonster", menuName = "Scriptable Objects/BossMonster")]
public class BossMonster : ScriptableObject
{
    public string monsterName;
    public string trait;
    public Sprite portrait;
    public int maxHp;
    public int attack;
}
