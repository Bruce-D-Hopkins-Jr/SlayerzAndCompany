using UnityEngine;

public static class SkillCardEffectHandler_Priest
{
    public static void ApplyEffect(SkillCard card, MonoBehaviour target = null)
    {
        if (card is PriestSkillCard priestCard)
        {
            switch (priestCard.PriestSkill)
            {
                case PriestSkill.BlessingOfVigor:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        HeroCombatController hero = target.GetComponent<HeroCombatController>();
                        hero.Heal(card.EffectValue);
                    }
                    break;

                case PriestSkill.HolyLight:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        HeroCombatController hero = target.GetComponent<HeroCombatController>();
                        hero.Heal(card.EffectValue);
                    }
                    break;

                case PriestSkill.Prayer:
                    HandManager.Instance.DrawCards(DeckManager.Instance.Draw(card.EffectValue));
                    break;

                case PriestSkill.Consecration:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        foreach (var m in EncounterManager.Instance.GetActiveEncounterMonsters())
                        {
                            m.TakeDamage(card.EffectValue);
                        }
                    }
                    break;

                case PriestSkill.DivineGrace:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        var heroList = GameObject.FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None);
                        foreach (var h in heroList)
                        {
                            h.Heal(card.EffectValue);
                        }
                    }
                    break;

                case PriestSkill.DivineIntervention:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        var heroList = GameObject.FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None);
                        foreach (var h in heroList)
                        {
                            h.Heal(card.EffectValue);
                        }
                    }
                    break;
            }
        }
    }
}
