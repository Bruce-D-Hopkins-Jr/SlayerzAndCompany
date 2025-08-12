using UnityEngine;

public static class SkillCardEffectHandler
{
    public static void ApplyEffect(SkillCard card, MonoBehaviour target = null)
    {
        switch (card.HeroType)
        {
            case HeroType.Mage:
                SkillCardEffectHandler_Mage.ApplyEffect(card, target);
                break;
            case HeroType.Priest:
                SkillCardEffectHandler_Priest.ApplyEffect(card, target);
                break;
            case HeroType.Scout:
                SkillCardEffectHandler_Scout.ApplyEffect(card, target);
                break;
            case HeroType.Warrior:
                SkillCardEffectHandler_Warrior.ApplyEffect(card, target);
                break;
        }
    }
}
