using UnityEngine;

[CreateAssetMenu(fileName = "UtilityCard", menuName = "Cards/UtilityCard")]
public class UtilityCard : Card
{
    [SerializeField, TextArea] private string description;

    public string Description => description;
}
