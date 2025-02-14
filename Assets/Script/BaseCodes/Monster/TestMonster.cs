using UnityEngine;

public class TestMonster : MonsterBase
{
    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            IDamagable i = hit.transform.GetComponent<IDamagable>();
            if (i == null)
                return;
            i.TakeDamage(1, EAtackElement.Normal);
        }
    }
}
