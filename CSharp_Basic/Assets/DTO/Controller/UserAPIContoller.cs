using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharp_Basic.Assets.DTO.Model
{
    public class UserAPIContoller
    {
        public void PrintAPI(UserAPI user)
        {
            Console.WriteLine($"User를 API로 제공: {user.id}");

            // dotnet add package Newtonsoft.Json // json 패키지 설치 (터미널)

            string s = JsonConvert.SerializeObject(user);
            Console.WriteLine(s);
        }
    }
}