using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] SceneName sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerController>().enabled)
        {
            return;
        }
        SceneLoadManager.Instance.MoveScene(sceneName);
    }
}
