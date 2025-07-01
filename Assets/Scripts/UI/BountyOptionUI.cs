using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BountyOptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private Button selectButton;

    private BossMonster bountyData;

    public event Action<BossMonster> OnSelected;

    public void Setup(BossMonster bounty)
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

    public BossMonster BountyData => bountyData;
}
