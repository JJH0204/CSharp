using System.Runtime.InteropServices.JavaScript;

namespace WebApplication1.Models
{
    public class ApplicationConfigReceivePacket : ReceivePacketBase
    {
        // 서버에서 환경에 따라 내려준 주소를 사용하려고 한다.
        // enviranment 개발 환경
        // os type - Android, IOS, PC
        // version - 1.0.0

        public int Environment_Type;
        public int OS_Type;
        public string AppVersion;

        public ApplicationConfigReceivePacket(string packetName, int E_Environment_Type, int E_OS_Type, string appVersion) : base(packetName)
        {
            this.Environment_Type = (int)E_Environment_Type;
            this.OS_Type = (int)E_OS_Type;
            this.AppVersion = appVersion;
        }
    }

    public class ApplicationConfigSendPacket : SendPacketBase
    {
        public string ApiUrl;

        public ApplicationConfigSendPacket(PACKET_NAME_TYPE packetName, RETURN_CODE returnCode, string apiUrl) : base(packetName, returnCode)
        {
            this.ApiUrl = apiUrl;
        }
    }
}
