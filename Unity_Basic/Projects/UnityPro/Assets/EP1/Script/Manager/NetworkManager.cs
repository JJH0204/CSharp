using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CertHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // Always accept
        return true;
    }
}

public class SendPacketBase
{
    public readonly string PacketName;

    public SendPacketBase(PACKET_NAME_TYPE packetName)
    {
        PacketName = packetName.ToString();
    }
}

public class ReceivePacketBase
{
    public readonly int ReturnCode;

    public ReceivePacketBase(int returnCode)
    {
        ReturnCode = returnCode;
    }
}

public class ApplicationConfigSendPacket: SendPacketBase
{
    // 서버에서 환경에 따라 내려준 주소를 사용하려고 한다.
    // enviranment 개발 환경
    // os type - Android, IOS, PC
    // version - 1.0.0

    public int Environment_Type;
    public int OS_Type;
    public string AppVersion;

    public ApplicationConfigSendPacket(PACKET_NAME_TYPE packetName, ENVIRONMENT_TYPE E_Environment_Type, OS_TYPE E_OS_Type, string appVersion) : base(packetName)
    {
        this.Environment_Type = (int)E_Environment_Type;
        this.OS_Type = (int)E_OS_Type;
        this.AppVersion = appVersion;
    }
}

public class ApplicationConfigReceivePacket: ReceivePacketBase
{
    public readonly string ApiUrl;

    public ApplicationConfigReceivePacket(int returnCode, string apiUrl) : base(returnCode)
    {
        this.ApiUrl = apiUrl;
    }
}

public class NetworkManager : ManagerBase
{
    private static NetworkManager instance = null;
    private string apiUrl;

    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NetworkManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SystemManager");
                    instance = obj.AddComponent<NetworkManager>();
                }
            }
            return instance;
        }
    }

    public bool IsInit { get; set; } = false;

    void Awake()
    {
        DontDestroy<NetworkManager>();
    }

    public void SetInit(string apiUrl)
    {
        IsInit = true;
        this.apiUrl = apiUrl;
    }

    public void SendPacket()
    {
        StartCoroutine(C_ConnectToServer());
    }

    private IEnumerator C_ConnectToServer()
    {
        ApplicationConfigSendPacket sendPacket = new ApplicationConfigSendPacket(
            PACKET_NAME_TYPE.ApplicationConfig,
            Config.E_ENVIRONMENT_TYPE,
            Config.E_OS_TYPE,
            Config.APP_VERSION);

        string packet = JsonUtility.ToJson(sendPacket);
        Debug.Log("[Send packet]: " + packet);

        // using 으로 UnityWebRequest 객체를 생성, 사용 후 자동으로 해제
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiUrl, packet))
        {
            // http 통신을 위한 POST 요청 생성성
            byte[] bytes = new System.Text.UTF8Encoding().GetBytes(packet);
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // HTTPS 요청을 위한 추가 설정: Ignore SSL certificate errors
            request.certificateHandler = new CertHandler();

            // Debug.Log("Connected to server: " + apiUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log("Connected to server: " + apiUrl);

                string jsonData = request.downloadHandler.text;
                Debug.Log("Received data: " + jsonData);
            }
        }
    }
}
