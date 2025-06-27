using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BountySelectionManager : MonoBehaviour
{
    public List<BossMonster> allBounties;
    public GameObject bountyOptionPrefab;
    public Transform bountyContainer;
    public Button confirmButton;

    private BossMonster selectedBoss;

    void Start()
    {
        confirmButton.interactable = false;
        confirmButton.onClick.AddListener(ConfirmBounty);

        GenerateBountyOptions();
    }

    void GenerateBountyOptions()
    {
        List<BossMonster> choices = new();
        //Note: Get rid of this in order to chose 3 random bosses
        int bossIndex = 0;

        while (choices.Count < 3)
        {
            //Note change to [Random.Range(0, allBounties.Count)]
            var pick = allBounties[bossIndex];
            if (!choices.Contains(pick)) choices.Add(pick);

            bossIndex++;
        }

        foreach (var boss in choices)
        {
            var option = Instantiate(bountyOptionPrefab, bountyContainer);
            var ui = option.GetComponent<BountyOptionUI>();
            ui.Setup(boss, this);
        }
    }    

    void ConfirmBounty()
    {
        GameManager.Instance.StoreBountySelection(selectedBoss);
        Debug.Log($"Selected Boss: {selectedBoss.monsterName}");

        DeckManager deckManager = FindAnyObjectByType<DeckManager>();
        deckManager.BuildDeck(GameManager.Instance.draftedHeroes);

        GameManager.Instance.LoadScene();

        // Proceed to battle loop
    }

    public void SelectBounty(BossMonster bounty)
    {
        selectedBoss = bounty;
        confirmButton.interactable = true;
    }

    public BossMonster GetSelectedBoss()
    {
        return selectedBoss;
    }
}
