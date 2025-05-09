public class SendPacketBase
{
    public string URL;
    public string PacketName;

    public SendPacketBase(string URL, PACKET_NAME_TYPE packetName)
    {
        this.URL = URL;
        PacketName = packetName.ToString();
    }
}

public class ReceivePacketBase
{
    public int ReturnCode;

    public ReceivePacketBase(int returnCode)
    {
        ReturnCode = returnCode;
    }
}