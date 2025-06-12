using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayCardUI : CardUI
{
    private PlayCard playCardData;

    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardName;
    // public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI effectText;
    // public Image cardArt;

    public void Setup(Card card)
    {
        cardType.text = card.cardType.ToString();
        cardName.text = card.cardName;

        if (card is PlayCard play)
        {
            effectText.text = play.currentEffectValue.ToString();
            playCardData = play;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (eventData.pointerEnter == null) return;

        if (playCardData.effectType == PlayCardType.DAMAGE && eventData.pointerEnter.CompareTag("Monster"))
        {
            GameManager.Instance.ApplyDamagePlayCard(playCardData);
            Destroy(gameObject);
        }
        else if (playCardData.effectType == PlayCardType.HEAL && eventData.pointerEnter.CompareTag("Player"))
        {
            GameManager.Instance.ApplyHealPlayCard(playCardData);
            Destroy(gameObject);
        }
    }

    public PlayCard GetPlayCardData()
    {
        return playCardData;
    }
}
