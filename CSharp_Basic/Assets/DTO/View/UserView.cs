using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp_Basic.Assets.DTO.Model;

namespace CSharp_Basic.Assets.DTO.View
{
    public class UserView
    {
        public void PrintUI(UserEntity user)
        {
            Console.WriteLine($"User ID 화면에 출력: {user.id}");
        }
    }
}