using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string ATTACK = "ATTACK";
    private const string HIT = "HIT";
    private const string RUN = "RUN";

    public void PlayAttack() => animator.SetTrigger(ATTACK);  
    public void PlayHit() => animator.SetTrigger(HIT);
    public void PlayRun(bool isRunning) => animator.SetBool(RUN, isRunning);
}
