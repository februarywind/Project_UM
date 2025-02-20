using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlaneTimer : MonoBehaviour
{
    public bool IsPlay { get; private set; }

    [SerializeField] DecalProjector warningProjector;
    [SerializeField] DecalProjector areaProjector;

    private void Awake()
    {
        warningProjector.enabled = false;
        areaProjector.enabled = false;
    }

    public void SetTimer(Vector3 pos, float time, float radius, Action action)
    {
        transform.position = pos;

        // Projector의 size는 width, height, depth
        areaProjector.size = new Vector3(radius, radius, 0.1f);
        warningProjector.size = Vector3.forward * 0.1f;

        StartCoroutine(TimerStart(time, radius, action));
    }
    IEnumerator TimerStart(float time, float radius, Action action)
    {
        warningProjector.enabled = true;
        areaProjector.enabled = true;
        IsPlay = true;

        float r = 0;
        Vector3 size = warningProjector.size;
        while (radius > r)
        {
            r += 1 / time * radius * Time.deltaTime;
            size.x = r; size.y = r;
            warningProjector.size = size;

            yield return null;
        }

        action?.Invoke();

        warningProjector.enabled = false;
        areaProjector.enabled = false;
        IsPlay = false;
    }
}
