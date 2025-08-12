using UnityEngine;

public static class SkillCardEffectHandler_Mage
{
    public static void ApplyEffect(SkillCard card, MonoBehaviour target = null)
    {
        if (card is MageSkillCard mageCard)
        {
            switch (mageCard.MageSkill)
            {
                case MageSkill.MagicBolt:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        MonsterCombatController monster = target.GetComponent<MonsterCombatController>();
                        monster.TakeDamage(card.EffectValue);
                    }
                    break;

                case MageSkill.Focus:
                    HandManager.Instance.DrawCards(DeckManager.Instance.Draw(card.EffectValue));
                    break;

                case MageSkill.ManaSurge:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        HeroCombatController hero = target.GetComponent<HeroCombatController>();
                        hero.Heal(card.EffectValue);
                    }
                    break;

                case MageSkill.FlameWave:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        foreach (var m in EncounterManager.Instance.GetActiveEncounterMonsters())
                        {
                            m.TakeDamage(card.EffectValue);
                        }
                    }
                    break;

                case MageSkill.ArcaneSense:
                    HandManager.Instance.DrawCards(DeckManager.Instance.Draw(card.EffectValue));
                    break;

                case MageSkill.MeteorStrike:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        MonsterCombatController monster = target.GetComponent<MonsterCombatController>();
                        monster.TakeDamage(card.EffectValue);
                    }
                    break;
            }
        }
    }
}
