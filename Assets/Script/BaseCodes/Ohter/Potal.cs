using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] int temp;

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == LayerMask.GetMask("Playable"))
        {
            SceneManager.LoadScene(temp);
        }
    }
}
