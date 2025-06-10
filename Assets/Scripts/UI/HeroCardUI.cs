using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardUI : MonoBehaviour
{
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
        }
    }
    
}
