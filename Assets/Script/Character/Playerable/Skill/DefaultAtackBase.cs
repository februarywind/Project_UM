using UnityEngine;

public class DefaultAtackBase : MonoBehaviour
{
    public bool IsActionUse { get; set; }

    private PlayerController controller;

    private Animator animator;

    private bool isCombo;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }
    public virtual void DefaultAtackActivate()
    {
        if (!isCombo)
        {
            isCombo = true;
            animator.SetTrigger("Atack");
            return;
        }
        animator.SetTrigger("NextCombo");
    }
    private void AtackEnd()
    {
        isCombo = false;
    }
}
