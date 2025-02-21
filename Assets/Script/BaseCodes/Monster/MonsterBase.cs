using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour, IDamagable
{
    public event Action<float> HpChange;

    public float MaxHp => maxHp;
    public float CurHp => curHp;

    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;

    protected Transform killer;
    private bool isDead;

    Dictionary<EAtackElement, Color> effectColor = new Dictionary<EAtackElement, Color>
    {
        { EAtackElement.Normal, Color.white },
        { EAtackElement.Electric, Color.yellow },
        { EAtackElement.Grass, Color.green },
    };

    private void OnEnable()
    {
        curHp = maxHp;
        isDead = false;
    }

    public void TakeDamage(float damage, EAtackElement element, Transform transform)
    {
        if (isDead) return;
        curHp -= damage;
        if (curHp < 1)
        {
            isDead = true;
            killer = transform;
            Dead();
        }
        DamagePopUpManager.instance.ShowDamagePopUp(this.transform.position + Vector3.up * 0.5f, $"{damage:F0}", effectColor[element]);
        HpChange?.Invoke(curHp);
    }
    protected virtual void Dead()
    {
        gameObject.SetActive(false);
    }
}
