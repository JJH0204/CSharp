using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets.DTO.Model
{
    // 로컬 저장 모델
    public class UserEntity
    {
        public readonly int id;
        public readonly string name;
        public readonly string desc;
        public readonly string password;

        public UserEntity(int id, string name, string desc, string password)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.password = password;
        }

        public UserDto ToDto()
        {
            return new UserDto(id, name, desc, password);
        }
    }
}