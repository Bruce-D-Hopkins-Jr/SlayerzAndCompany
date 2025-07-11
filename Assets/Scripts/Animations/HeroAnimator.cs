using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string ATTACK = "ATTACK";

    public void PlayAttack() => animator.SetTrigger(ATTACK);    
}
