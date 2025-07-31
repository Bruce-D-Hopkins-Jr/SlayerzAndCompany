using UnityEngine;

public static class SkillCardEffectHandler_Warrior
{
    public static void ApplyEffect(SkillCard card, MonoBehaviour target = null)
    {
        if (card is WarriorSkillCard warriorCard)
        {
            switch (warriorCard.WarriorSkill)
            {
                case WarriorSkill.PowerStrike:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        MonsterCombatController monster = target.GetComponent<MonsterCombatController>();
                        monster.TakeDamage(card.EffectValue);
                    }
                    break;

                case WarriorSkill.BattleInstinct:
                    HandManager.Instance.DrawCards(DeckManager.Instance.Draw(card.EffectValue));
                    break;

                case WarriorSkill.SecondWind:
                    if (target.GetComponent<HeroCombatController>() != null)
                    {
                        HeroCombatController hero = target.GetComponent<HeroCombatController>();
                        hero.Heal(card.EffectValue);
                    }
                    break;

                case WarriorSkill.Earthshatter:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        foreach (var m in EncounterManager.Instance.GetActiveEncounterMonsters())
                        {
                            m.TakeDamage(card.EffectValue);
                        }
                    }
                    break;

                case WarriorSkill.Whirlwind:
                    if (target.GetComponent<MonsterCombatController>() != null)
                    {
                        foreach (var m in EncounterManager.Instance.GetActiveEncounterMonsters())
                        {
                            m.TakeDamage(card.EffectValue);
                        }
                    }
                    break;

                case WarriorSkill.ShoulderBash:
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
