using UnityEngine;

public static class UtilityCardEffectHandler
{
    public static void ApplyEffect(UtilityCard card, MonoBehaviour target = null)
    {
        switch (card.EffectType)
        {
            case UtilityEffectType.Heal:
                if (target.GetComponent<HeroCombatController>() != null)
                {
                    HeroCombatController hero = target.GetComponent<HeroCombatController>();
                    hero.Heal(card.EffectValue);
                }                    
                break;

            case UtilityEffectType.HealAOE:
                if (target.GetComponent<HeroCombatController>() != null)
                {
                    foreach (var h in GameObject.FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None))
                    {
                        h.Heal(card.EffectValue);
                    }                        
                }                    
                break;

            case UtilityEffectType.Damage:
                if (target.GetComponent<MonsterCombatController>() != null)
                {
                    MonsterCombatController monster = target.GetComponent<MonsterCombatController>();
                    monster.TakeDamage(card.EffectValue);
                }                    
                break;

            case UtilityEffectType.DamageAOE:
                if (target.GetComponent<MonsterCombatController>() != null)
                {
                    foreach (var m in EncounterManager.Instance.GetActiveEncounterMonsters())
                    {
                        m.TakeDamage(card.EffectValue);
                    }                        
                }                
                break;

            case UtilityEffectType.DrawCards:
                HandManager.Instance.DrawCards(DeckManager.Instance.Draw(card.EffectValue));
                break;
        }

        Debug.Log($"[UtilityEffectHandler] {card.CardName} effect applied: {card.EffectType} ({card.EffectValue})");
    }

}
