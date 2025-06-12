using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroCardUI : CardUI
{
    private HeroCard heroCardData;

    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardName;
    // public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ATKText;

    // public Image cardArt;

    public void Setup(Card card)
    {
        cardType.text = card.cardType.ToString();
        cardName.text = card.cardName;
        
        if (card is HeroCard hero)
        {
            HPText.text = hero.currentHitPoints.ToString();
            ATKText.text = hero.currentAttackPoints.ToString();
            heroCardData = hero;
        }
    }
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("HeroPosition"))
        {
            GameManager.Instance.SummonHero(heroCardData, eventData.pointerEnter.transform);
            Destroy(gameObject); // Remove UI card
        }
    }

    public HeroCard GetHeroCardData()
    {
        return heroCardData;
    }
}
