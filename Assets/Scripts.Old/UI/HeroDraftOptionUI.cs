using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroDraftOptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heroTypeText;
    [SerializeField] private Button selectButton;

    private Hero heroData;

    public event System.Action<Hero> OnHeroSelected;

    public void Setup(Hero hero)
    {
        heroData = hero;
        heroTypeText.text = hero.HeroType.ToString();

        selectButton.onClick.AddListener(() =>
        {
            OnHeroSelected?.Invoke(heroData);
        });
    }

    public void DisableSelection()
    {
        selectButton.interactable = false;
    }

    public Hero HeroData => heroData;
}
