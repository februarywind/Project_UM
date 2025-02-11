using UnityEngine;

public class DefaultAtackBase : MonoBehaviour
{
    private Animator animator;

    private bool isCombo;
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
