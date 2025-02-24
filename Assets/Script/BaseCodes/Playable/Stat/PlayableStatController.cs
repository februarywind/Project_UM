using System.Collections;
using UnityEngine;

public class PlayableStatController : MonoBehaviour, IDamagable
{
    [SerializeField] PlayableStat stat;
    public PlayableStat Stat => stat;

    private Coroutine stamina;

    private void Start()
    {
        stat.CurStamina = stat.MaxStamina;
        stat.OnChangeCurHp += Stat_OnChangeCurHp;
    }

    private void Stat_OnChangeCurHp(float obj)
    {

    }

    public void TakeDamage(float damage, EAtackElement element, Transform transform)
    {
        if (stat.IsInvincibility)
            return;

        stat.CurHp -= damage;
        DamagePopUpManager.instance.ShowDamagePopUp(transform.position, $"{damage}", Color.red);
    }
    public void StaminaRegen()
    {
        StaminaRegenStop();
        stamina = StartCoroutine(StaminaRegenRoutine());
    }
    public void StaminaRegenStop()
    {
        if (stamina != null)
        {
            StopCoroutine(stamina);
        }
    }
    IEnumerator StaminaRegenRoutine()
    {
        yield return Util.GetDelay(stat.StaminaRegenWaitTime);
        while (stat.MaxStamina > stat.CurStamina)
        {
            stat.CurStamina += stat.RunStaminaVFS * Time.deltaTime;
            yield return null;
        }
    }
}
