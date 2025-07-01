using UnityEngine;

[CreateAssetMenu(fileName = "BossMonster", menuName = "Scriptable Objects/BossMonster")]
public class BossMonster : Monster
{    
    [SerializeField] private string trait;

    public string Trait => trait;
}
