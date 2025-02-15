using System.Collections;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public void CoroutineAgent(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
