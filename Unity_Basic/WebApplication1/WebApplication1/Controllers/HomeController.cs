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
                    case PACKET_NAME_TYPE.Maintenance:
                        sendPacket = Maintenance(json);
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

        private SendPacketBase Maintenance(string json)
        {
            // 점검 패킷 처리
            MaintenanceReceivePacket? receivePacket = JsonConvert.DeserializeObject<MaintenanceReceivePacket>(json);
            if (receivePacket == null)
            {
                return new SendPacketBase(PACKET_NAME_TYPE.Maintenance, RETURN_CODE.ERROR);
            }

            // 점검 패킷 생성
            PACKET_NAME_TYPE packetNameType = PACKET_NAME_TYPE.None;
            if (Enum.TryParse(receivePacket.PacketName, out PACKET_NAME_TYPE type))
                packetNameType = type;
            MaintenanceSendPacket? sendPacket = null;

            // 점검 여부
            bool isMaintenance = false;
            if (CompareVersions(receivePacket.AppVersion, "1.0.3") < 0) // 버전 비교
            {
                isMaintenance = true;
                // 점검 멘트
                string title = string.Empty;
                string contents = string.Empty;
                switch ((LANGUAGE_TYPE)receivePacket.languageType)
                {
                    case LANGUAGE_TYPE.Korean:
                        title = "현재 점검중입니다.";
                        contents = "점검이 완료되면 다시 시도해 주세요.";
                        break;
                    case LANGUAGE_TYPE.Japanese:
                        title = "現在点検中です。";
                        contents = "点検が完了したら、もう一度お試しください。";
                        break;
                    case LANGUAGE_TYPE.Chinese:
                        title = "目前正在检查中。";
                        contents = "检查完成后，请重试。";
                        break;
                    default:
                        title = "Currently under maintenance.";
                        contents = "Please try again after the maintenance is completed.";
                        break;
                }

                sendPacket = new MaintenanceSendPacket(packetNameType, RETURN_CODE.OK, isMaintenance, title, contents);
                return sendPacket;
            }
            
            sendPacket = new MaintenanceSendPacket(packetNameType, RETURN_CODE.OK, isMaintenance, string.Empty, string.Empty);

            return sendPacket;
        }

        /// <summary>
        /// 버전 비교
        /// </summary>
        private int CompareVersions(string version1, string version2)
        {
            string[] version1Parts = version1.Split('.');
            string[] version2Parts = version2.Split('.');

            for (int i = 0; i < Math.Max(version1Parts.Length, version2Parts.Length); i++)
            {
                int part1 = i < version1Parts.Length ? int.Parse(version1Parts[i]) : 0;
                int part2 = i < version2Parts.Length ? int.Parse(version2Parts[i]) : 0;

                if (part1 > part2)
                    return 1;
                else if (part1 < part2)
                    return -1;
            }

            return 0;
        }

        private SendPacketBase ApplicationConfig(string json)
        {
            ApplicationConfigReceivePacket? receivePacket = JsonConvert.DeserializeObject<ApplicationConfigReceivePacket>(json);
            if (receivePacket == null)
            {
                return new SendPacketBase(PACKET_NAME_TYPE.ApplicationConfig, RETURN_CODE.ERROR);
            }

            string apiURL = string.Empty;
            switch ((ENVIRONMENT_TYPE)receivePacket.Environment_Type)
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

            // 권한 설정
            DEVELOPER_ID_AUTHORITY developerIdAuthority = DEVELOPER_ID_AUTHORITY.NONE;

            // TODO: receivePacket.DevelopmentID = null 인 문제 해결
            if (receivePacket.DevelopmentID == "1q2w3e4r")
            {
                developerIdAuthority = DEVELOPER_ID_AUTHORITY.TESTER;
            }

            PACKET_NAME_TYPE packetNameType = PACKET_NAME_TYPE.None;
            if (Enum.TryParse(receivePacket.PacketName, out PACKET_NAME_TYPE type))
                packetNameType = type;

            ApplicationConfigSendPacket sendPacket = new ApplicationConfigSendPacket(packetNameType, RETURN_CODE.OK, apiURL, developerIdAuthority);
            //UserInfo userInfo = new UserInfo(1, "John Doe");
            //string packet = JsonConvert.SerializeObject(applicationConfigSendPacket);  // Json ����ȭ
            return sendPacket;
        }
    }
}
