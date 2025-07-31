using UnityEngine;

public static class SkillCardEffectHandler_Scout
{
    public static void ApplyEffect(SkillCard card, MonoBehaviour target = null)
    {
        if (card is ScoutSkillCard scoutCard)
        {
            switch (scoutCard.ScoutSkill)
            {
                case ScoutSkill.Backstab:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        MonsterCombatController monster = target.GetComponent<MonsterCombatController>();
                        monster.TakeDamage(card.EffectValue);
                    }
                    break;

                case ScoutSkill.LightStep:
                    HandManager.Instance.DrawCards(DeckManager.Instance.Draw(card.EffectValue));
                    break;

                case ScoutSkill.QuickSlash:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        MonsterCombatController monster = target.GetComponent<MonsterCombatController>();
                        monster.TakeDamage(card.EffectValue);
                    }
                    break;

                case ScoutSkill.Ambush:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        foreach (var m in EncounterManager.Instance.GetActiveEncounterMonsters())
                        {
                            m.TakeDamage(card.EffectValue);
                        }
                    }
                    break;

                case ScoutSkill.PartyRations:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        var heroList = GameObject.FindObjectsByType<HeroCombatController>(FindObjectsSortMode.None);
                        foreach (var h in heroList)
                        {
                            h.Heal(card.EffectValue);
                        }
                    }
                    break;

                case ScoutSkill.ReconPatch:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        HeroCombatController hero = target.GetComponent<HeroCombatController>();
                        hero.Heal(card.EffectValue);
                    }
                    break;
            }
        }
    }
}
