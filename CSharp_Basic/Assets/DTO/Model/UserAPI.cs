using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets.DTO.Model
{
    public class UserAPI
    {
        public readonly int id;
        public readonly string name;
        public readonly string desc;

        public UserAPI(int id, string name, string desc)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
        }

        public UserDto ToDto()
        {
            return new UserDto(id, name, desc, "");
        }
    }
}