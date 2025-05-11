public class ApplicationConfigSendPacket : SendPacketBase
{
    // 서버에서 환경에 따라 내려준 주소를 사용하려고 한다.
    // enviranment 개발 환경
    // os type - Android, IOS, PC
    // version - 1.0.0

    public int Environment_Type;
    public int OS_Type;
    public string AppVersion;
    public string DeveloperId;    // 개발자 ID

    public ApplicationConfigSendPacket(string URL, PACKET_NAME_TYPE packetName, ENVIRONMENT_TYPE E_Environment_Type, OS_TYPE E_OS_Type, string appVersion, string DeveloperId) : base(URL, packetName)
    {
        this.Environment_Type = (int)E_Environment_Type;
        this.OS_Type = (int)E_OS_Type;
        this.AppVersion = appVersion;
        this.DeveloperId = DeveloperId;
    }
}

public class ApplicationConfigReceivePacket : ReceivePacketBase
{
    public string ApiUrl;
    public int DeveloperIdAuthority;    // 개발자 ID 권한

    public ApplicationConfigReceivePacket(int returnCode, string apiUrl, int DeveloperIdAuthority) : base(returnCode)
    {
        this.ApiUrl = apiUrl;
        this.DeveloperIdAuthority = DeveloperIdAuthority;
    }
}
