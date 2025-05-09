using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

// 간단한 C# 게임 서버 코드
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
            // 클라이언트에서 Post로 전송한 데이터가 없을 경우
            if (Request.ContentLength == 0)
            {
                sendPacket = new SendPacketBase(PACKET_NAME_TYPE.None, RETURN_CODE.ERROR);
                string sendData = JsonConvert.SerializeObject(sendPacket);
                return Content(sendData);
            }

            string json = string.Empty;
            // 클라이언트에서 Post로 전송한 데이터 읽음
            using (var reader = new StreamReader(Request.Body))
            {
                json = await reader.ReadToEndAsync();
            }
            // 먼저 패킷 이름을 읽어와서 검사
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
                // 패킷 이름이 잘못된 경우
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
            //string packet = JsonConvert.SerializeObject(applicationConfigSendPacket);  // Json 직렬화
            return applicationConfigSendPacket;
        }
    }
}
