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
        public IActionResult Index()
        {
            UserInfo userInfo = new UserInfo(1, "John Doe");
            string packet = JsonConvert.SerializeObject(userInfo);  // Json 직렬화
            return Content(packet);
        }
    }
}
