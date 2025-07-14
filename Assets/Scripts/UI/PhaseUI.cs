using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI phaseLabel;

    private void Awake()
    {
        endTurnButton.onClick.AddListener(OnEndTurnClicked);
        endTurnButton.gameObject.SetActive(false);
    }

    public void UpdateUI(GamePhase phase)
    {
        phaseLabel.text = $"Phase: {phase}";

        // Only show button during SLAY phase
        endTurnButton.gameObject.SetActive(phase == GamePhase.SLAY);
    }

    private void OnEndTurnClicked()
    {
        PhaseManager.Instance.SetCurrentPhase(GamePhase.MONSTER);
        PhaseManager.Instance.AdvancePhase(); // SLAY → MONSTER
    }
}
