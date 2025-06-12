using UnityEngine;
using UnityEngine.EventSystems;

public enum DropType { HERO_POSITION, MONSTER, PLAYER }

public class DropTarget : MonoBehaviour, IDropHandler
{
    public DropType dropType;

    public void OnDrop(PointerEventData eventData)
    {
        var cardUI = eventData.pointerDrag.GetComponent<CardUI>();
        if (cardUI == null) return;

        switch (dropType)
        {
            case DropType.HERO_POSITION:
                HeroCardUI heroCardUI = cardUI.GetComponent<HeroCardUI>();
                if (heroCardUI != null)
                {
                    GameManager.Instance.SummonHero(heroCardUI.GetHeroCardData(), transform);
                }
                break;

            case DropType.MONSTER:
                PlayCardUI damageCardUI = cardUI.GetComponent<PlayCardUI>();
                if (damageCardUI != null && damageCardUI.GetPlayCardData().effectType == PlayCardType.DAMAGE)
                {
                    GameManager.Instance.ApplyDamagePlayCard(damageCardUI.GetPlayCardData());
                }
                break;

            case DropType.PLAYER:
                PlayCardUI healCardUI = cardUI.GetComponent<PlayCardUI>();
                if (healCardUI != null && healCardUI.GetPlayCardData().effectType == PlayCardType.HEAL)
                {
                    GameManager.Instance.ApplyHealPlayCard(healCardUI.GetPlayCardData());
                }
                break;
        }
    }
}
