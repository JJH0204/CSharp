namespace WebApplication1.Models
{
    public class ReceivePacketBase
    {
        public string PacketName;

        public ReceivePacketBase(string packetName)
        {
            PacketName = packetName.ToString();
        }
    }

    public class SendPacketBase
    {
        public string PacketName = string.Empty;
        public int ReturnCode;

        public SendPacketBase(string packetName,int returnCode)
        {
            PacketName = packetName;
            ReturnCode = returnCode;
        }
    }
}
