using UnityEngine;

public class SkillCardEffectHandler
{
    public static void ApplyEffect(SkillCard card, MonoBehaviour target = null)
    {
        switch (card.HeroType)
        {
            case HeroType.Mage:
                break;
            case HeroType.Priest:
                break;
            case HeroType.Scout:
                break;
            case HeroType.Warrior:
                break;
        }
    }
}
