using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp_Basic.Assets.DTO.Model;
using CSharp_Basic.Assets.DTO.View;

namespace CSharp_Basic.Assets.DTO.Controller
{
    internal class UserController
    {
        public void PrintWindow()
        {
            UserEntity userEntity = new UserEntity(123, "재호", "프로그래머", "1234123");
            UserView userView = new UserView();
            userView.PrintUI(userEntity);
        }
    }
}