using UnityEngine;

public class PlayableStatController : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerBattleStat stat;
    public PlayerBattleStat Stat => stat;


    private void Awake()
    {
        stat.OnChangeCurHp += Stat_OnChangeCurHp;
    }

    private void Stat_OnChangeCurHp(float obj)
    {
        //Debug.Log(obj);
    }

    public void TakeDamage(float damage, EAtackElement element)
    {
        stat.CurHp -= damage;
        DamagePopUpManager.instance.ShowDamagePopUp(transform.position, $"{damage}", Color.red);
    }
}
