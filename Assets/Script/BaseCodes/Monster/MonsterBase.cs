using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour, IDamagable
{
    public event Action<float> HpChange;

    public float MaxHp => maxHp;
    public float CurHp => curHp;

    [SerializeField] float maxHp;
    [SerializeField] float curHp;

    Dictionary<EAtackElement, Color> effectColor = new Dictionary<EAtackElement, Color> 
    { 
        { EAtackElement.Normal, Color.white }, 
        { EAtackElement.Electric, Color.yellow }, 
        { EAtackElement.Grass, Color.green }, 
    };

    private void OnEnable()
    {
        curHp = maxHp;
    }

    public void TakeDamage(float damage, EAtackElement element, Transform transform)
    {
        curHp -= damage;
        if (curHp < 1)
        {
            Dead();
        }
        DamagePopUpManager.instance.ShowDamagePopUp(transform.position, $"{damage}", effectColor[element]);
        HpChange?.Invoke(curHp);
    }
    protected virtual void Dead()
    {
        gameObject.SetActive(false);
    }
}
