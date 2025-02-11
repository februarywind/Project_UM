using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class RelayManager : MonoBehaviour
{
    [SerializeField] Button joinButton;
    [SerializeField] TMP_InputField codeTMP;
    [ContextMenu("CreateRoom")]
    private async void CreateRoom()
    {
        await CreateRelay(2);
    }
    [ContextMenu("JoinRoom")]
    private async void JoinRoom()
    {
        await JoinRelay(codeTMP.text);
    }
    private async void Start()
    {
        await InitializeUnityServices();
        joinButton.onClick.AddListener(JoinRoom);
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                // 1. Unity Services 초기화
                await UnityServices.InitializeAsync();

                // 2. 익명 로그인 (필수)
                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }

                Debug.Log("Unity Services 초기화 완료!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Unity Services 초기화 실패: {e.Message}");
        }
    }

    public async Task<string> CreateRelay(int maxConnections)
    {
        try
        {
            // Unity Services가 초기화되지 않았으면 초기화 실행
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await InitializeUnityServices();
            }

            // Relay 서버 생성
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);

            // Join Code 생성
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log($"Relay 서버 생성 완료! Region: {allocation.Region} Join Code: {joinCode}");

            // Unity Transport 설정
            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            // Netcode 호스트 시작
            NetworkManager.Singleton.StartHost();

            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay 서버 생성 실패: {e.Message}");
            return null;
        }
    }
    public async Task JoinRelay(string joinCode)
    {
        try
        {
            // 1. Join Code를 사용하여 Relay 서버 접속 정보 가져오기
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            // 2. Unity Transport 가져오기
            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            // 3. Relay 서버 데이터 설정 (올바른 방식)
            transport.SetRelayServerData(
                allocation.RelayServer.IpV4,        // 서버 IP
                (ushort)allocation.RelayServer.Port, // 서버 포트
                allocation.AllocationIdBytes,       // 할당 ID
                allocation.Key,                      // 보안 키
                allocation.ConnectionData,           // 연결 데이터
                allocation.HostConnectionData        // 호스트 연결 데이터
            );

            // 4. Netcode 클라이언트 시작
            NetworkManager.Singleton.StartClient();

            Debug.Log("Relay 서버 접속 성공!");
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Relay 서버 접속 실패: {e.Message}");
        }
    }
}