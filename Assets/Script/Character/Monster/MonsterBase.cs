using UnityEngine;

public class MonsterBase : MonoBehaviour, IDamagable
{
    [SerializeField] float maxHp;
    [SerializeField] float curHp;

    private void OnEnable()
    {
        curHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        if (curHp < 1)
        {
            Dead();
        }
        DamagePopUpManager.instance.ShowDamagePopUp(transform.position, $"{damage}", Color.white);
    }
    protected virtual void Dead()
    {
        gameObject.SetActive(false);
    }
}
