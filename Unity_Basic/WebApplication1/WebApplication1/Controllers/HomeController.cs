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

    public class HomeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            string json = string.Empty;
            // 클라이언트에서 Post로 전송한 데이터 읽음
            using (var reader = new StreamReader(Request.Body))
            {
                json = await reader.ReadToEndAsync();
            }

            ApplicationConfigReceivePacket? applicationConfigReceivePacket = JsonConvert.DeserializeObject<ApplicationConfigReceivePacket>(json);

            ApplicationConfigSendPacket applicationConfigSendPacket = new ApplicationConfigSendPacket(applicationConfigReceivePacket.PacketName, 200, "https://localhost:7025/");
            //UserInfo userInfo = new UserInfo(1, "John Doe");
            string packet = JsonConvert.SerializeObject(applicationConfigSendPacket);  // Json 직렬화
            return Content(packet);
        }
    }
}
