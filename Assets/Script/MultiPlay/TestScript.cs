using Unity.Netcode;

public class TestScript : NetworkBehaviour
{
    private void Awake()
    {
        if (!IsOwner)
        {
            GetComponent<PlayerController>().enabled = false;
        }
    }
}
