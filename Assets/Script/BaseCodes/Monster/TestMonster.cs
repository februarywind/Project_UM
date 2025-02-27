using System.Collections;
using UnityEngine;

public class TestMonster : MonsterBase
{
    protected override void Dead()
    {
        base.Dead();
        killer.GetComponent<PlayableStatController>().Stat.LevelUp();
        EffectManager.instance.ParticlePlay("Buff", 3f, killer.position, Quaternion.identity, killer);
        Invoke("ReSpwan", 5f);
    }
    private void ReSpwan()
    {
        maxHp += 0;
        gameObject.SetActive(true);
    }
}
