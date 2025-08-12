using System.Collections;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string ATTACK = "ATTACK";
    private const string HIT = "HIT";

    public void PlayAttack() => animator.SetTrigger(ATTACK);
    public void PlayHit(float timeDelay) => StartCoroutine(DelayHit(timeDelay));

    private IEnumerator DelayHit(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        animator.SetTrigger(HIT);
    }
}
