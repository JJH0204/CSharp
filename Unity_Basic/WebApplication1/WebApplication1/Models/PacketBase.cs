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

        public SendPacketBase(PACKET_NAME_TYPE packetName,RETURN_CODE returnCode)
        {
            PacketName = packetName.ToString();
            ReturnCode = (int)returnCode;
        }
    }
}
