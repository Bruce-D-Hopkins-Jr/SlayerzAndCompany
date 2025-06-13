using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextPhaseButton : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    public static NextPhaseButton Instance;

    private void Start()
    {
        if (nextButton == null)
            nextButton = GetComponent<Button>();

        nextButton.onClick.AddListener(AdvancePhase);
    }

    private void Update()
    {
        UpdateButtonText();
    }

    private void AdvancePhase()
    {
        GameManager.Instance.AdvancePhaseManually();
    }

    private void UpdateButtonText()
    {
        if (GameManager.Instance == null || buttonText == null) return;

        string nextPhase = PredictNextPhase(GameManager.Instance.GetCurrentPhase());
        buttonText.text = $"Next Phase: {nextPhase} PHASE";
    }

    private string PredictNextPhase(GameManager.GamePhase current)
    {
        return current switch
        {
            GameManager.GamePhase.DRAW => "PLAY",
            GameManager.GamePhase.PLAY => "SLAY",
            GameManager.GamePhase.SLAY => "MONSTERTURN",
            GameManager.GamePhase.MONSTER => "DRAW",
            _ => "UNKNOWN"
        };
    }

}