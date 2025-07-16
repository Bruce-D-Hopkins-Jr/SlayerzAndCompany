using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Sprite cardArtImage;
    [SerializeField] private TextMeshProUGUI cardNameText;    
    [SerializeField] private TextMeshProUGUI cardDescriptionText;

    public void Setup(Card card)
    {
        cardArtImage = card.Art;
        cardNameText.text = card.CardName;

        switch (card)
        {
            case SkillCard skill:
                cardDescriptionText.text = skill.Description;                
                break;

            case UtilityCard utility:
                cardDescriptionText.text = utility.Description;                
                break;

            default:
                cardDescriptionText.text = "";
                break;
        }
    }
}
