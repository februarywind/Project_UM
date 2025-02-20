using UnityEngine;

public class PlayableStatController : MonoBehaviour, IDamagable
{
    [SerializeField] PlayableStat stat;
    public PlayableStat Stat => stat;

    private void Start()
    {

        stat.OnChangeCurHp += Stat_OnChangeCurHp;
    }

    private void Stat_OnChangeCurHp(float obj)
    {

    }

    public void TakeDamage(float damage, EAtackElement element)
    {
        if (stat.IsInvincibility)
            return;

        stat.CurHp -= damage;
        DamagePopUpManager.instance.ShowDamagePopUp(transform.position, $"{damage}", Color.red);
    }
}
