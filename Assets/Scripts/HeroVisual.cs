using UnityEngine;

public class HeroVisual : MonoBehaviour
{
    [SerializeField] private GameObject selectionRing;
    [SerializeField] private Renderer ringRenderer;

    [Header("Ring Colors")]
    [SerializeField] private Color availableColor = Color.blue;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color usedColor = Color.red;

    public void ShowRing(bool show)
    {
        if (selectionRing != null)
            selectionRing.SetActive(show);
    }

    public void SetRingState(HeroRingState state)
    {
        if (ringRenderer == null) return;

        switch (state)
        {
            case HeroRingState.Available:
                ringRenderer.material.color = availableColor;
                break;
            case HeroRingState.Selected:
                ringRenderer.material.color = selectedColor;
                break;
            case HeroRingState.Used:
                ringRenderer.material.color = usedColor;
                break;
        }
    }
}

public enum HeroRingState
{
    Available,
    Selected,
    Used
}
