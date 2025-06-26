using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroDraftOptionUI : MonoBehaviour
{
    public TextMeshProUGUI heroTypeText;
    public Button selectButton;

    private Hero heroData;
    private HeroDraftManager heroDraftManager;

    public void Setup(Hero hero, HeroDraftManager manager)
    {
        heroData = hero;
        heroDraftManager = manager;
        heroTypeText.text = hero.heroType.ToString();

        selectButton.onClick.AddListener(() =>
        {
            if(heroDraftManager.GetSelectedHeroes().Count < 3)
            {
                heroDraftManager.SelectHero(heroData);
                selectButton.interactable = false;
            }            
        });
    }
}
