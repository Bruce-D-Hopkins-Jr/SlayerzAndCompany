using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BountyOptionUI : MonoBehaviour
{
    public TextMeshProUGUI bossNameText;
    public Button selectButton;

    private BossMonster monsterData;
    private BountySelectionManager manager;

    public void Setup(BossMonster bounty, BountySelectionManager bountyManager)
    {
        monsterData = bounty;
        manager = bountyManager;

        bossNameText.text = bounty.monsterName.ToString();

        selectButton.onClick.AddListener(() => 
        {
            if (!manager.GetSelectedBoss())
            {
                manager.SelectBounty(monsterData);
                selectButton.interactable = false;
            }            
        });
                  
                  
    }
}
