using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountySelectionManager : MonoBehaviour
{
    [SerializeField] private List<Monster> allBounties;
    [SerializeField] private GameObject bountyOptionPrefab;
    [SerializeField] private Transform bountyContainer;
    [SerializeField] private Button confirmButton;

    private Monster selectedBoss;
    private List<BountyOptionUI> bountyUIOptions = new();

    private void Start()
    {
        confirmButton.interactable = false;
        confirmButton.onClick.AddListener(ConfirmBounty);

        GenerateBountyOptions();
    }

    private void GenerateBountyOptions()
    {
        List<Monster> choices = new();
        int bossIndex = 0;

        while (choices.Count < 3)
        {
            var pick = allBounties[bossIndex];
            if (!choices.Contains(pick)) choices.Add(pick);
            bossIndex++;
        }

        foreach (var boss in choices)
        {
            var optionGO = Instantiate(bountyOptionPrefab, bountyContainer);
            var bountyUI = optionGO.GetComponent<BountyOptionUI>();

            bountyUI.Setup(boss);
            bountyUI.OnSelected += HandleBountySelected;

            bountyUIOptions.Add(bountyUI);
        }
    }

    private void HandleBountySelected(Monster bounty)
    {
        if (selectedBoss != null) return;

        selectedBoss = bounty;
        confirmButton.interactable = true;

        BountyOptionUI ui = bountyUIOptions.Find(ui => ui.BountyData == bounty);
        ui?.DisableSelection();

        Debug.Log($"Selected Boss: {bounty.MonsterName}");
    }

    private void ConfirmBounty()
    {
        GameManager.Instance.StoreBountySelection(selectedBoss);

        DeckManager deckManager = FindAnyObjectByType<DeckManager>();
        deckManager.BuildDeck(GameManager.Instance.draftedHeroes);

        GameManager.Instance.LoadScene();
    }

    public Monster GetSelectedBoss() => selectedBoss;
}
