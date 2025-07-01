using UnityEngine;

[CreateAssetMenu(fileName = "UtilityCard", menuName = "Cards/UtilityCard")]
public class UtilityCard : Card
{
    [SerializeField, TextArea] private string effect;

    public string Effect => effect;
}
