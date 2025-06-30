using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public string monsterName;
    public int maxHP;
    public int attack;
    public Sprite portrait;
    public GameObject monsterPrefab;
}
