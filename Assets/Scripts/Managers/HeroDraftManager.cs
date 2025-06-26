using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeroDraftManager : MonoBehaviour
{
    public List<Hero> heroesList;
    public GameObject heroOptionPrefab;
    public GameObject heroOptionUI;
    public GameObject bountyOptionUI;
    public Transform heroOptionsContainer;
    public Button continueButton;

    private List<Hero> selectedHeroes = new List<Hero>();

    private void Start()
    {
        GenerateDraftOptions();
        continueButton.interactable = false;
        continueButton.onClick.AddListener(ConfirmSelection);
    }

    void GenerateDraftOptions()
    {
        List<Hero> pool = new List<Hero>();
        //Note: Get rid of this to in order to choose out of 5 randomly selected heroes
        int poolIndex = 0;

        while (pool.Count < 4)
        {
            //Note: Change to [Random.Range(0, heroesList.Count)]
            var hero = heroesList[poolIndex];
            if (!pool.Contains(hero))
            {
                pool.Add(hero);
            }

            poolIndex++;
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

        GameManager.Instance.StoreDraftedHeroes(selectedHeroes);

        heroOptionUI.SetActive(false);
        bountyOptionUI.SetActive(true);
    }

    public void SelectHero(Hero hero)
    {
        if (selectedHeroes.Contains(hero) || selectedHeroes.Count >= 3) return;

        selectedHeroes.Add(hero);
        Debug.Log($"Selected: {hero.heroType}");

        if (selectedHeroes.Count == 3)
        {
            continueButton.interactable = true;
        }
    }

    public List<Hero> GetSelectedHeroes()
    {
        return selectedHeroes;
    }
}
