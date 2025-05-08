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

    public void SendPacket(SendPacketBase sendPacket)
    {
        StartCoroutine(C_ConnectToServer(sendPacket));
    }

    private IEnumerator C_ConnectToServer(SendPacketBase sendPacket)
    {
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
                // Debug.Log("Connected to server: " + apiUrl);

                string jsonData = request.downloadHandler.text;
                Debug.Log("Received data: " + jsonData);

                ApplicationConfigReceivePacket applicationConfigReceivePacket = JsonUtility.FromJson<ApplicationConfigReceivePacket>(jsonData);

                // Debug.Log("ReturnCode: " + applicationConfigReceivePacket.ReturnCode);
                // Debug.Log("ApiUrl: " + applicationConfigReceivePacket.ApiUrl);
            }
        }
    }
}
