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

    public void SendPacket()
    {
        StartCoroutine(C_ConnectToServer());
    }

    private IEnumerator C_ConnectToServer()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        request.certificateHandler = new CertHandler(); // Ignore SSL certificate errors

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
