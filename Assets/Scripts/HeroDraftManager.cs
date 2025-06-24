using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeroDraftManager : MonoBehaviour
{
    public List<HeroCard> heroesList;
    public GameObject heroOptionPrefab;
    public Transform heroOptionsContainer;
    public Button continueButton;

    private List<HeroCard> selectedHeroes = new List<HeroCard>();

    private void Start()
    {
        GenerateDraftOptions();
        continueButton.interactable = false;
        continueButton.onClick.AddListener(ConfirmSelection);
    }

    void GenerateDraftOptions()
    {
        List<HeroCard> pool = new List<HeroCard>();

        while (pool.Count < 5)
        {
            var hero = heroesList[Random.Range(0, heroesList.Count)];
            if (!pool.Contains(hero))
            {
                pool.Add(hero);
            }
        }

        foreach (var hero in pool)
        {
            GameObject option = Instantiate(heroOptionPrefab, heroOptionsContainer);
            var ui = option.GetComponent<HeroDraftOptionUI>();
            ui.Setup(hero, this);
        }
    }

    void ConfirmSelection()
    {
        Debug.Log("Final selection confirmed!");
        foreach (var hero in selectedHeroes)
        {
            Debug.Log($"- {hero.heroType}");
        }
    }

    public void SelectHero(HeroCard hero)
    {
        if (selectedHeroes.Contains(hero) || selectedHeroes.Count >= 3) return;

        selectedHeroes.Add(hero);
        Debug.Log($"Selected: {hero.heroType}");

        if (selectedHeroes.Count == 3)
        {
            continueButton.interactable = true;
        }
    }
}
