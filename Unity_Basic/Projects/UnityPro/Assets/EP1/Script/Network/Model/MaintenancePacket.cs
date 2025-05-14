// 점검 패킷
using UnityEngine;

public class MaintenanceSendPacket : SendPacketBase
{
    public int Environment_Type;
    public int OS_Type;
    public string AppVersion;
    public int languageType;    // 다국어 처리 

    public MaintenanceSendPacket(string URL,
        PACKET_NAME_TYPE packetName,
        ENVIRONMENT_TYPE E_Environment_Type,
        OS_TYPE E_OS_Type,
        string appVersion,
        SystemLanguage languageType) : base(URL, packetName)
    {
        this.Environment_Type = (int)E_Environment_Type;
        this.OS_Type = (int)E_OS_Type;
        this.AppVersion = appVersion;
        this.languageType = (int)languageType;
    }
}

public class MaintenanceReceivePacket : ReceivePacketBase
{
    public string ApiUrl;
    public int DeveloperIdAuthority;    // 개발자 ID 권한
    public bool isMaintenance;    // 점검 여부
    public string Title;    // 점검 제목
    public string Contents;    // 점검 내용

    public MaintenanceReceivePacket(int returnCode, string apiUrl, bool isMaintenance, string title, string contents) : base(returnCode)
    {
        this.ApiUrl = apiUrl;
        this.DeveloperIdAuthority = DeveloperIdAuthority;
        this.isMaintenance = isMaintenance;
        this.Title = title;
        this.Contents = contents;
    }
}