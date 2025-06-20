﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles UI and drag-drop interaction for a hero card.
/// </summary>
public class HeroCardUI : CardUI
{
    [Header("UI References")]
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ATKText;

    private HeroCard heroCardData;

    /// <summary>
    /// Initializes the UI elements for a hero card.
    /// </summary>
    public void Setup(Card card)
    {
        cardType.text = card.cardType.ToString();
        cardName.text = card.cardName;

        if (card is HeroCard hero)
        {
            HPText.text = hero.currentHitPoints.ToString();
            ATKText.text = hero.currentAttackPoints.ToString();
            heroCardData = hero;
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.activePlayer.playedHero)
        {
            Debug.Log("Hero already played, cannot drag.");
            return;
        }

        base.OnBeginDrag(eventData); // ✅ only if all checks pass
    }

    /// <summary>
    /// Handles drop logic by raycasting into the world to detect valid targets.
    /// </summary>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        var player = GameManager.Instance.activePlayer;

        if (player.playedHero)
        {
            Debug.Log("You have already played a Hero this turn.");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("HeroPosition"))
            {
                DropTarget dropZone = hit.collider.GetComponent<DropTarget>();
                if (dropZone != null && dropZone.gameObject.transform.childCount == 0)
                {
                    dropZone.ReceiveHeroDrop(heroCardData);
                    Destroy(gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Returns the HeroCard data associated with this UI instance.
    /// </summary>
    public HeroCard GetHeroCardData()
    {
        return heroCardData;
    }
}