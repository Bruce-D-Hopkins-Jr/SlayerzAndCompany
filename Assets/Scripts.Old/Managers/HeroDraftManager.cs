using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeroDraftManager : MonoBehaviour
{
    [SerializeField] private List<Hero> heroesList;
    [SerializeField] private GameObject heroOptionPrefab;
    [SerializeField] private Transform heroOptionsContainer;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject heroOptionUI;
    [SerializeField] private GameObject bountyOptionUI;

    private List<Hero> selectedHeroes = new();
    private List<HeroDraftOptionUI> uiOptions = new();

    private void Start()
    {
        GenerateDraftOptions();
        continueButton.interactable = false;
        continueButton.onClick.AddListener(ConfirmSelection);
    }

    private void GenerateDraftOptions()
    {
        List<Hero> pool = new();

        int poolIndex = 0;
        while (pool.Count < 4)
        {
            var hero = heroesList[poolIndex];
            if (!pool.Contains(hero))
                pool.Add(hero);

            poolIndex++;
        }

        foreach (var hero in pool)
        {
            GameObject optionGO = Instantiate(heroOptionPrefab, heroOptionsContainer);
            var optionUI = optionGO.GetComponent<HeroDraftOptionUI>();

            optionUI.Setup(hero);
            optionUI.OnHeroSelected += HandleHeroSelection;

            uiOptions.Add(optionUI);
        }
    }

    private void HandleHeroSelection(Hero hero)
    {
        if (selectedHeroes.Contains(hero) || selectedHeroes.Count >= 3) return;

        selectedHeroes.Add(hero);
        Debug.Log($"Selected: {hero.HeroType}");

        // Disable UI for selected hero
        HeroDraftOptionUI ui = uiOptions.Find(ui => ui.HeroData == hero);
        ui?.DisableSelection();

        if (selectedHeroes.Count == 3)
        {
            continueButton.interactable = true;
        }
    }

    private void ConfirmSelection()
    {
        Debug.Log("Final selection confirmed!");
        foreach (var hero in selectedHeroes)
        {
            Debug.Log($"- {hero.HeroType}");
        }

        GameManager.Instance.StoreDraftedHeroes(selectedHeroes);
        heroOptionUI.SetActive(false);
        bountyOptionUI.SetActive(true);
    }

    public List<Hero> GetSelectedHeroes() => selectedHeroes;
}
