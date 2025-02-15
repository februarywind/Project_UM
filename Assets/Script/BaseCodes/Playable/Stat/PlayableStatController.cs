using UnityEngine;

public class PlayableStatController : MonoBehaviour, IDamagable
{
    [SerializeField] PlayableStat stat;
    public PlayableStat Stat => stat;


    private void Start()
    {
        // 여기서 데이터 초기화를 한다.


        stat.OnChangeCurHp += Stat_OnChangeCurHp;
    }

    private void Stat_OnChangeCurHp(float obj)
    {

    }

    public void TakeDamage(float damage, EAtackElement element)
    {
        stat.CurHp -= damage;
        DamagePopUpManager.instance.ShowDamagePopUp(transform.position, $"{damage}", Color.red);
    }
}
