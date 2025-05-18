using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class CertHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // Always accept
        return true;
    }
}

public class NetworkManager : ManagerBase
{
    #region Singleton
    private static NetworkManager instance = null;
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NetworkManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("NetworkManager");
                    instance = obj.AddComponent<NetworkManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    #region Variables
    private string apiUrl;
    public bool IsInit { get; set; } = false;
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<NetworkManager>();
    }
    #endregion

    #region Methods
    public void SetInit(string apiUrl)
    {
        IsInit = true;
        this.apiUrl = apiUrl;
    }

    public IEnumerator NetworkManagerInit()
    {
        yield return null;
        // 클라 -> 서버: 패킷 생성
        ApplicationConfigSendPacket sendPacket = new ApplicationConfigSendPacket(
            Config.SERVER_APP_CONFIG_URL,
            PACKET_NAME_TYPE.ApplicationConfig,
            Config.E_ENVIRONMENT_TYPE,
            Config.E_OS_TYPE,
            Config.APP_VERSION,
            SystemManager.Instance.DevelopmentID    // 개발자 ID
            );
        // 클라 -> 서버 : 패킷 전송
        IEnumerator enumerator = CoroutineSendPacket<ApplicationConfigReceivePacket>(sendPacket);
        yield return StartCoroutine(enumerator);

        // 서버 -> 클라: 패킷 수신
        ApplicationConfigReceivePacket receivePacket = enumerator.Current is ApplicationConfigReceivePacket packet ? packet : null;

        // 서버 응답 확인
        if (receivePacket != null && receivePacket.ReturnCode == (int)RETURN_CODE.OK)
        {
            // 서버에서 응답이 성공적으로 왔다면
            SystemManager.Instance.ApiUrl = receivePacket.ApiUrl;    // 서버에서 내려준 주소를 사용
            SystemManager.Instance.developerIdAuthority = (DEVELOPER_ID_AUTHORITY)receivePacket.DeveloperIdAuthority;
            yield return true;
        }
        else
        {
            // 서버에서 응답이 실패했다면
            yield return false;
        }
    }

    // public void SendPacket(SendPacketBase sendPacket)
    // {
    //     StartCoroutine(CoroutineSendPacket(sendPacket));
    // }

    /// <summary>
    /// 비동기 패킷 전송을 위한 코루틴 메서드
    /// </summary>
    public IEnumerator CoroutineSendPacket<T>(SendPacketBase sendPacket) where T : ReceivePacketBase
    {
        string packet = JsonUtility.ToJson(sendPacket);
        Debug.Log("[Send packet]: " + packet);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiUrl, packet))
        {
            byte[] bytes = new System.Text.UTF8Encoding().GetBytes(packet);
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.certificateHandler = new CertHandler();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                Debug.LogError("Response Code: " + request.responseCode);
                Debug.LogError("Response Text: " + request.downloadHandler.text);

                // 추가 디버깅 로그
                Debug.LogError("API URL: " + apiUrl);
                Debug.LogError("Request Headers: " + string.Join(", ", request.GetRequestHeader("Content-Type")));

                yield return null;
            }
            else
            {
                string jsonData = request.downloadHandler.text;
                Debug.Log("Received data: " + jsonData);

                T receivePacket = null;
                try
                {
                    receivePacket = JsonUtility.FromJson<T>(jsonData);
                    if (receivePacket == null)
                    {
                        Debug.LogError("Failed to parse JSON into packet. JSON: " + jsonData);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Exception while parsing JSON: " + ex.Message);
                    Debug.LogError("JSON Data: " + jsonData);
                }

                yield return receivePacket;
            }
        }
    }

    /// <summary>
    /// 점검 여부 확인 패킷을 송수신
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoroutineMaintenanceSendPacket()
    {
        MaintenanceSendPacket sendPacket = new MaintenanceSendPacket(
            SystemManager.Instance.ApiUrl,
            PACKET_NAME_TYPE.Maintenance,
            Config.E_ENVIRONMENT_TYPE,
            Config.E_OS_TYPE,
            Config.APP_VERSION,
            Application.systemLanguage    // 다국어 처리
            );

        IEnumerator enumerator = CoroutineSendPacket<MaintenanceReceivePacket>(sendPacket);
        yield return StartCoroutine(enumerator);

        yield return enumerator.Current is MaintenanceReceivePacket packet ? packet : null;
    }
    #endregion
}
