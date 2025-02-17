using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    [ContextMenu("Write")]
    private async void Temp()
    {
        var data = new Dictionary<string, object> { { "MySaveKey", "HelloWorld" } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        // 해쉬를 키로하는 딕셔너리가 반환되는듯하다?
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
          "firstKeyName", "secondKeyName"
        });

    }
}
