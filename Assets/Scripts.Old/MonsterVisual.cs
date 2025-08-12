using UnityEngine;

public class MonsterVisual : MonoBehaviour
{
    [SerializeField] private GameObject targetIndicator;

    public void ShowTargetIndicator(bool show)
    {
        if (targetIndicator != null)
            targetIndicator.SetActive(show);
    }
}
