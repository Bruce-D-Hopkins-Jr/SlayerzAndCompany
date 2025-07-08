using UnityEngine;

public class HeroVisual : MonoBehaviour
{
    [SerializeField] private GameObject selectionRing;
    [SerializeField] private Renderer selectionRingRenderer;
    [SerializeField] private GameObject targetIndicator;

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
        if (selectionRingRenderer == null) return;

        switch (state)
        {
            case HeroRingState.Available:
                selectionRingRenderer.material.color = availableColor;
                break;
            case HeroRingState.Selected:
                selectionRingRenderer.material.color = selectedColor;
                break;
            case HeroRingState.Used:
                selectionRingRenderer.material.color = usedColor;
                break;
        }
    }

    public void ShowTargetIndicator(bool show)
    {
        if (targetIndicator != null)
            targetIndicator.SetActive(show);
    }
}


public enum HeroRingState
{
    Available,
    Selected,
    Used
}
