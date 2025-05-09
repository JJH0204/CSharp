using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

// ������ C# ���� ���� �ڵ�
namespace WebApplication1.Controllers
{
    public class UserInfo
    {
        public readonly int UserID;
        public readonly string UserName;

        public UserInfo(int userID, string userName)
        {
            UserID = userID;
            UserName = userName;
        }
    }
    /// <summary>
    /// [SERVER]
    /// </summary>
    public class HomeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            SendPacketBase sendPacket = null;
            // Ŭ���̾�Ʈ���� Post�� ������ �����Ͱ� ���� ���
            if (Request.ContentLength == 0)
            {
                sendPacket = new SendPacketBase(PACKET_NAME_TYPE.None, RETURN_CODE.ERROR);
                string sendData = JsonConvert.SerializeObject(sendPacket);
                return Content(sendData);
            }

            string json = string.Empty;
            // Ŭ���̾�Ʈ���� Post�� ������ ������ ����
            using (var reader = new StreamReader(Request.Body))
            {
                json = await reader.ReadToEndAsync();
            }
            // ���� ��Ŷ �̸��� �о�ͼ� �˻�
            ReceivePacketBase? receivePacketBase = JsonConvert.DeserializeObject<ReceivePacketBase>(json);

            
            if (receivePacketBase != null && Enum.TryParse(receivePacketBase.PacketName, out PACKET_NAME_TYPE type))
            {
                switch (type)
                {
                    case PACKET_NAME_TYPE.ApplicationConfig:
                        sendPacket = ApplicationConfig(json);
                        break;
                    default:
                        sendPacket = new SendPacketBase(PACKET_NAME_TYPE.None, RETURN_CODE.ERROR);
                        string sendData = JsonConvert.SerializeObject(sendPacket);
                        return Content(sendData);
                        //break;
                }
            }
            else
            {
                // ��Ŷ �̸��� �߸��� ���
                sendPacket = new SendPacketBase(PACKET_NAME_TYPE.None, RETURN_CODE.ERROR);
                string sendData = JsonConvert.SerializeObject(sendPacket);
                return Content(sendData);
            }

            
            return Content(JsonConvert.SerializeObject(sendPacket));
        }

        private SendPacketBase ApplicationConfig(string json)
        {
            ApplicationConfigReceivePacket? applicationConfigReceivePacket = JsonConvert.DeserializeObject<ApplicationConfigReceivePacket>(json);
            if (applicationConfigReceivePacket == null)
            {
                return new SendPacketBase(PACKET_NAME_TYPE.None, RETURN_CODE.ERROR);
            }

            string apiURL = string.Empty;
            switch ((ENVIRONMENT_TYPE)applicationConfigReceivePacket.Environment_Type)
            {
                case ENVIRONMENT_TYPE.DEV:
                    apiURL = "https://localhost:7025/";
                    break;
                case ENVIRONMENT_TYPE.STAGE:
                    apiURL = "https://localhost:7025/";
                    break;
                case ENVIRONMENT_TYPE.LIVE:
                    apiURL = "https://localhost:7025/";
                    break;
                default:
                    apiURL = "https://localhost:7025/";
                    break;
            }
            PACKET_NAME_TYPE packetNameType = PACKET_NAME_TYPE.None;
            if (Enum.TryParse(applicationConfigReceivePacket.PacketName, out PACKET_NAME_TYPE type))
                packetNameType = type;

            ApplicationConfigSendPacket applicationConfigSendPacket = new ApplicationConfigSendPacket(packetNameType, RETURN_CODE.OK, apiURL);
            //UserInfo userInfo = new UserInfo(1, "John Doe");
            //string packet = JsonConvert.SerializeObject(applicationConfigSendPacket);  // Json ����ȭ
            return applicationConfigSendPacket;
        }
    }
}
