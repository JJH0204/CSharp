using WebApplication1.Models;

public class MaintenanceReceivePacket : ReceivePacketBase
{
    public int Environment_Type;
    public int OS_Type;
    public string AppVersion;
    public int languageType;    // 다국어 처리 

    public MaintenanceReceivePacket(
        string packetName,
        int E_Environment_Type,
        int E_OS_Type,
        string appVersion,
        int languageType) : base(packetName)
    {
        this.Environment_Type = (int)E_Environment_Type;
        this.OS_Type = (int)E_OS_Type;
        this.AppVersion = appVersion;
        this.languageType = (int)languageType;
    }
}

public class MaintenanceSendPacket : SendPacketBase
{ 
    public bool isMaintenance;    // 점검 여부
    public string Title;    // 점검 제목
    public string Contents;    // 점검 내용

    public MaintenanceSendPacket(PACKET_NAME_TYPE packetName,RETURN_CODE returnCode, bool isMaintenance, string title, string contents) : base(packetName, returnCode)
    {
        this.isMaintenance = isMaintenance;
        this.Title = title;
        this.Contents = contents;
    }
}