using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseUI : MonoBehaviour
{
    [SerializeField] private Button endPhaseButton;
    [SerializeField] private TextMeshProUGUI phaseLabel;

    private void Awake()
    {
        endPhaseButton.onClick.AddListener(OnEndTurnClicked);
        endPhaseButton.gameObject.SetActive(false);
    }

    public void UpdateUI(GamePhase phase)
    {
        phaseLabel.text = $"Phase: {phase}";

        // Only show button during SLAY phase
        endPhaseButton.gameObject.SetActive(phase == GamePhase.SLAY || phase == GamePhase.PLAY);
    }

    private void OnEndTurnClicked()
    {
        GamePhase currentPhase = PhaseManager.Instance.CurrentPhase;
        if (currentPhase == GamePhase.PLAY)
        {
            PhaseManager.Instance.SetCurrentPhase(GamePhase.SLAY);
            PhaseManager.Instance.AdvancePhase(); // PLAY → SLAY
        }
        else if (currentPhase == GamePhase.SLAY)
        {
            PhaseManager.Instance.SetCurrentPhase(GamePhase.MONSTER);
            PhaseManager.Instance.AdvancePhase(); // SLAY → MONSTER
        }
    }
}
