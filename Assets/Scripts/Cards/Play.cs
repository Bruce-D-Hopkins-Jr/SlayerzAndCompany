using UnityEngine;

public enum PlayCardType { HEAL, DAMAGE }

[CreateAssetMenu(menuName = "Cards/PlayCard")]
public class PlayCard : Card
{
    public PlayCardType effectType;
    public int minEffectValue;
    public int maxEffectValue;

    [HideInInspector] public int currentEffectValue;

    public void InitializeValue()
    {
        currentEffectValue = Random.Range(minEffectValue, maxEffectValue + 1);
    }
}
