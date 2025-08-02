using TMPro;
using UnityEngine;

public class CardCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deckCountText;

    private void Update()
    {
        if (DeckManager.Instance != null)
        {
            deckCountText.text = DeckManager.Instance.CardsRemaining().ToString();
        }
    }
}
