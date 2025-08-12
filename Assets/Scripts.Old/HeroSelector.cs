using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HeroSelector : MonoBehaviour
{
    private HeroCombatController combatController;

    private void Awake()
    {
        combatController = GetComponent<HeroCombatController>();

        if (combatController == null)
        {
            Debug.LogError("HeroSelector requires a HeroCombatController on the same GameObject.");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log($"[HeroSelector] {gameObject.name} was clicked.");

        if (PhaseManager.Instance.CurrentPhase != GamePhase.SLAY)
        {
            Debug.Log("[HeroSelector] Not in SLAY phase — ignoring click.");
            return;
        }

        var slayManager = FindAnyObjectByType<SlayManager>();
        if (slayManager != null)
        {
            Debug.Log($"[HeroSelector] Sending {combatController.HeroData.HeroType} to SlayManager.");
            slayManager.SelectHero(combatController);
        }
        else
        {
            Debug.LogError("[HeroSelector] SlayManager not found in scene.");
        }
    }
}

