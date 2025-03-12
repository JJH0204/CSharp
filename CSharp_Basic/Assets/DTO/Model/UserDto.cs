using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets.DTO.Model
{
    public class UserDto
    {
        public readonly int id;
        public readonly string name;
        public readonly string desc;
        public readonly string password;

        public UserDto(int id, string name, string desc, string password)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.password = password;
        }

        // DTO를 통해 데이터를 쉽게 바꿔 올 수 있도록 설정할 수 있다. 
        public UserEntity ToEntity()
        {
            return new UserEntity(id: id, name: name, desc: desc, password: password);
        }

        public UserAPI ToAPI()
        {
            return new UserAPI(id: id, name: name, desc: desc);
        }
    }
}