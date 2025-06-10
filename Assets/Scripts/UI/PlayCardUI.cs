using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardUI : MonoBehaviour
{
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
        }
    }
}
