using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BountyOptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private Button selectButton;

    private Monster bountyData;

    public event Action<Monster> OnSelected;

    public void Setup(Monster bounty)
    {
        bountyData = bounty;
        bossNameText.text = bounty.MonsterName;
        selectButton.onClick.AddListener(() =>
        {
            OnSelected?.Invoke(bountyData);
        });
    }

    public void DisableSelection()
    {
        selectButton.interactable = false;
    }

    public Monster BountyData => bountyData;
}
