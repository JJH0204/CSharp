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
                    GameObject obj = new GameObject("NetworkManager");
                    instance = obj.AddComponent<NetworkManager>();
                }
            }
            return instance;
        }
    }

    public bool IsInit { get; set; } = false;

    private InitScene_UI initSceneUI;

    void Awake()
    {
        DontDestroy<NetworkManager>();
        initSceneUI = FindObjectOfType<InitScene_UI>();
    }

    public void SetInit(string apiUrl)
    {
        IsInit = true;
        this.apiUrl = apiUrl;
    }

    // public void SendPacket(SendPacketBase sendPacket)
    // {
    //     StartCoroutine(C_SendPacket(sendPacket));
    // }

#if By_Call_Back
    // call-back
    public IEnumerator C_SendPacket<T>(SendPacketBase sendPacket, Action<ReceivePacketBase> action = null) where T : ReceivePacketBase
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
                yield return null;
                action?.Invoke(new ReceivePacketBase((int)RETURN_CODE.ERROR));
            }
            else
            {
                // Debug.Log("Connected to server: " + apiUrl);
                // 성공적으로 응답을 받았을 때
                string jsonData = request.downloadHandler.text;
                Debug.Log("Received data: " + jsonData);

                T receivePacket = JsonUtility.FromJson<T>(jsonData);
                yield return receivePacket;
                action?.Invoke(receivePacket);

                // Debug.Log("ReturnCode: " + applicationConfigReceivePacket.ReturnCode);
                // Debug.Log("ApiUrl: " + applicationConfigReceivePacket.ApiUrl);
            }
        }
    }
#else
    public IEnumerator C_SendPacket<T>(SendPacketBase sendPacket) where T : ReceivePacketBase
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

            initSceneUI.LoadingGear.OnEnable();

            // Debug.Log("Connected to server: " + apiUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                yield return null;
                initSceneUI.LoadingGear.OnDisable();
            }
            else
            {
                // Debug.Log("Connected to server: " + apiUrl);
                // 성공적으로 응답을 받았을 때
                string jsonData = request.downloadHandler.text;
                Debug.Log("Received data: " + jsonData);

                T receivePacket = JsonUtility.FromJson<T>(jsonData);
                yield return receivePacket;

                initSceneUI.LoadingGear.OnDisable();
                // return receivePacket;

                // Debug.Log("ReturnCode: " + applicationConfigReceivePacket.ReturnCode);
                // Debug.Log("ApiUrl: " + applicationConfigReceivePacket.ApiUrl);
            }
        }
    }
#endif
}
