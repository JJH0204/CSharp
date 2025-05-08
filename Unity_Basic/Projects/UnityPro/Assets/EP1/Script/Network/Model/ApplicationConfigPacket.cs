public class ApplicationConfigSendPacket : SendPacketBase
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

public class ApplicationConfigReceivePacket : ReceivePacketBase
{
    public string ApiUrl;

    public ApplicationConfigReceivePacket(int returnCode, string apiUrl) : base(returnCode)
    {
        this.ApiUrl = apiUrl;
    }
}
