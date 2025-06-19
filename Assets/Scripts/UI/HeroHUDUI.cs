using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeroHUDUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private Button attackButton;

    private HeroCard hero;

    private void Start()
    {
        if (gameObject.GetComponent<Canvas>() != null && gameObject.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
        {
            gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }

    public void Init(HeroCard card)
    {
        hero = card;

        attackButton.onClick.AddListener(OnAttackPressed);
        UpdateHUD();
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        // Show attack button only in SLAY phase and only if it's still enabled
        bool show = GameManager.Instance.GetCurrentPhase() == GameManager.GamePhase.SLAY;
        attackButton.gameObject.SetActive(show);

        transform.forward = Camera.main.transform.forward; // face camera
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (hero == null) return;

        hpText.text = $"HP: {hero.currentHitPoints}";
        atkText.text = $"ATK: {hero.currentAttackPoints}";
    }

    private void OnAttackPressed()
    {
        if (GameManager.Instance.GetCurrentPhase() != GameManager.GamePhase.SLAY) return;

        GameManager.Instance.currentMonster.currentHitPoints -= hero.currentAttackPoints;
        Debug.Log($"{hero.cardName} attacks for {hero.currentAttackPoints}!");        
    }
}
