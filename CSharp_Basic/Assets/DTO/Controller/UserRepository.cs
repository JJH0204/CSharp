using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets.DTO.Model
{
    public class UserRepository
    {
        public void Save(UserEntity user)
        {
            Console.WriteLine($"User를 DB에 저장: {user.id}");
        }
    }
}