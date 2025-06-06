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

    public PlayCard Clone()
    {
        PlayCard clone = ScriptableObject.CreateInstance<PlayCard>();
        clone.cardName = this.cardName;
        clone.cardType = this.cardType;
        clone.effectType = this.effectType;
        clone.minEffectValue = this.minEffectValue;
        clone.maxEffectValue = this.maxEffectValue;
        clone.currentEffectValue = this.currentEffectValue;

        clone.InitializeValue();

        return clone;
    }
}
