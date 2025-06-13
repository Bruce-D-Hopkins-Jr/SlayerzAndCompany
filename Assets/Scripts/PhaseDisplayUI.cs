using TMPro;
using UnityEngine;

public class PhaseDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI phaseText;

    private void Update()
    {
        if (GameManager.Instance == null || phaseText == null) return;

        string current = GameManager.Instance.GetCurrentPhase().ToString();
        phaseText.text = $"Current Phase: {current}";
    }
}
