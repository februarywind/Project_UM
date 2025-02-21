using System.Collections;
using UnityEngine;

public class TestMonster : MonsterBase
{
    protected override void Dead()
    {
        base.Dead();
        killer.GetComponent<PlayableStatController>().Stat.LevelUp();
        Invoke("ReSpwan", 5f);
    }
    private void ReSpwan()
    {
        maxHp += 100;
        gameObject.SetActive(true);
    }
}
