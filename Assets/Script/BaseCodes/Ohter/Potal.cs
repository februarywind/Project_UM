using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] SceneName sceneName;

    private void OnTriggerEnter(Collider other)
    {
        SceneLoadManager.Instance.MoveScene(sceneName);
    }
}
