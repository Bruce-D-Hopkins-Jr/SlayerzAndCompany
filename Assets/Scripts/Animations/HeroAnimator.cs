using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string ATTACK = "ATTACK";
    private const string HIT = "HIT";

    public void PlayAttack() => animator.SetTrigger(ATTACK);  
    public void PlayHit() => animator.SetTrigger(HIT);
}
