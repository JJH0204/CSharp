using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;
using Microsoft.Extensions.Logging;

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

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            if (Request.ContentLength == 0)
            {
                _logger.LogWarning("Received an empty request.");
                return CreateErrorResponse(PACKET_NAME_TYPE.None);
            }

            string json = await ReadRequestBodyAsync();
            _logger.LogInformation("Received request body: {RequestBody}", json);

            ReceivePacketBase? receivePacketBase = JsonConvert.DeserializeObject<ReceivePacketBase>(json);

            if (receivePacketBase == null || !Enum.TryParse(receivePacketBase.PacketName, out PACKET_NAME_TYPE type))
            {
                _logger.LogWarning("Invalid packet received: {PacketName}", receivePacketBase?.PacketName);
                return CreateErrorResponse(PACKET_NAME_TYPE.None);
            }

            _logger.LogInformation("Processing packet of type: {PacketType}", type);

            SendPacketBase sendPacket = type switch
            {
                PACKET_NAME_TYPE.ApplicationConfig => ApplicationConfig(json),
                PACKET_NAME_TYPE.Maintenance => Maintenance(json),
                _ => new SendPacketBase(PACKET_NAME_TYPE.None, RETURN_CODE.ERROR)
            };

            _logger.LogInformation("Response generated: {Response}", JsonConvert.SerializeObject(sendPacket));

            return Content(JsonConvert.SerializeObject(sendPacket));
        }

        private async Task<string> ReadRequestBodyAsync()
        {
            using var reader = new StreamReader(Request.Body);
            return await reader.ReadToEndAsync();
        }

        private IActionResult CreateErrorResponse(PACKET_NAME_TYPE packetNameType)
        {
            var sendPacket = new SendPacketBase(packetNameType, RETURN_CODE.ERROR);
            return Content(JsonConvert.SerializeObject(sendPacket));
        }

        private SendPacketBase Maintenance(string json)
        {
            MaintenanceReceivePacket? receivePacket = JsonConvert.DeserializeObject<MaintenanceReceivePacket>(json);
            if (receivePacket == null)
            {
                return new SendPacketBase(PACKET_NAME_TYPE.Maintenance, RETURN_CODE.ERROR);
            }

            bool isMaintenance = CompareVersions(receivePacket.AppVersion, "1.0.3") < 0;
            string title = string.Empty;
            string contents = string.Empty;

            if (isMaintenance)
            {
                (title, contents) = receivePacket.languageType switch
                {
                    (int)LANGUAGE_TYPE.Korean => ("현재 점검중입니다.", "점검이 완료되면 다시 시도해 주세요."),
                    (int)LANGUAGE_TYPE.Japanese => ("現在点検中です。", "点検が完了したら、もう一度お試しください。"),
                    (int)LANGUAGE_TYPE.Chinese => ("目前正在检查中。", "检查完成后，请重试。"),
                    _ => ("Currently under maintenance.", "Please try again after the maintenance is completed.")
                };
            }

            return new MaintenanceSendPacket(PACKET_NAME_TYPE.Maintenance, RETURN_CODE.OK, isMaintenance, title, contents);
        }

        private int CompareVersions(string version1, string version2)
        {
            string[] version1Parts = version1.Split('.');
            string[] version2Parts = version2.Split('.');

            for (int i = 0; i < Math.Max(version1Parts.Length, version2Parts.Length); i++)
            {
                int part1 = i < version1Parts.Length ? int.Parse(version1Parts[i]) : 0;
                int part2 = i < version2Parts.Length ? int.Parse(version2Parts[i]) : 0;

                if (part1 != part2)
                {
                    return part1.CompareTo(part2);
                }
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

            string apiURL = receivePacket.Environment_Type switch
            {
                (int)ENVIRONMENT_TYPE.DEV => "https://localhost:7025/",
                (int)ENVIRONMENT_TYPE.STAGE => "https://localhost:7025/",
                (int)ENVIRONMENT_TYPE.LIVE => "https://localhost:7025/",
                _ => "https://localhost:7025/"
            };

            DEVELOPER_ID_AUTHORITY developerIdAuthority = receivePacket.DevelopmentID == "1q2w3e4r"
                ? DEVELOPER_ID_AUTHORITY.TESTER
                : DEVELOPER_ID_AUTHORITY.NONE;

            return new ApplicationConfigSendPacket(PACKET_NAME_TYPE.ApplicationConfig, RETURN_CODE.OK, apiURL, developerIdAuthority);
        }
    }
}
