using UnityEngine;

public class DropTarget : MonoBehaviour
{
    public void ReceiveDrop(CardUI dragItem)
    {
        Debug.Log("Dropped onto world object: " + gameObject.name);
    }

    public void ReceiveHeroDrop(HeroCard heroData)
    {
        GameManager.Instance.SummonHero(heroData, transform);
    }

    public void ReceiveHeroDrop(PlayCard playData)
    {
        if (playData.effectType == PlayCardType.HEAL)
        {
            HeroCard targetHero = GetComponentInChildren<HeroReference>()?.heroCard;

            if (targetHero != null)
            {
                GameManager.Instance.ApplyHealPlayCard(playData, targetHero);
            }
            else
            {
                Debug.LogWarning("No hero card reference found on this drop target.");
            }
        }
    }

    public void ReceiveMonsterDrop(PlayCard playData)
    {
        if (playData.effectType == PlayCardType.DAMAGE)
        {
            GameManager.Instance.ApplyDamagePlayCard(playData);
        }
    }
}
