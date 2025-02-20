using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] PlaneTimer planeTimerPrefab;

    private BehaviorGraphAgent behaviorAgent;
    private PlayerController[] players;
    private MonsterBase monster;

    private Stack<PlaneTimer> timers = new(5);

    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();

        // 씬에서 모든 컴포넌트를 찾는 메서드 Include는 비활성화 오브젝트도 탐색, FindObjectsSortMode는 아마도 정렬같다.
        players = FindObjectsByType<PlayerController>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        monster = GetComponent<MonsterBase>();

        behaviorAgent.SetVariableValue("Players", players.Select(x => x.gameObject).ToList());

        hpSlider.maxValue = monster.MaxHp;
        hpSlider.value = monster.MaxHp;

        monster.HpChange += Monster_HpChange;

    }

    public void MonsterSkill(Vector3 pos, float waitTime, float radius, System.Action action)
    {
        foreach (var item in timers)
        {
            if (item.IsPlay) continue;
            item.SetTimer(pos, waitTime, radius, action);
            return;
        }
        timers.Push(Instantiate(planeTimerPrefab));
        timers.Peek().SetTimer(pos, waitTime, radius, action);
    }

    private void Monster_HpChange(float curHp)
    {
        hpSlider.value = curHp;
    }
}
