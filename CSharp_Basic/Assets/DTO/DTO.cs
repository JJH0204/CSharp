using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp_Basic.Assets.DTO.Model;

namespace CSharp_Basic.Assets.DTO
{
    public static class DTO
    {
        public static void DTOMain()
        {
            Console.WriteLine("DTO Main");

            // DTO (MVC, Entity, Repository, Nuget)

            // MVC 패턴 (Model, View, Controller)
            // Model: data
            // View: UI
            // Controller: Data <---> UI

            UserEntity userEntity = new UserEntity(id: 123, name: "jaeho", desc: "programmer", password: "123412");
            UserRepository userRepository = new UserRepository();
            userRepository.Save(userEntity);

            // DB에서 받은 데이터를 DTO로 저장
            // UserDto userDto = new UserDto(id: userEntity.id, name: userEntity.name, desc: userEntity.desc, password: userEntity.password);

            // UserAPI userAPI = new UserAPI(id: userEntity.id, name: userEntity.name, desc: userEntity.desc);
            UserAPIContoller userAPIContoller = new UserAPIContoller();
            userAPIContoller.PrintAPI(userEntity.ToDto().ToAPI());

        }
    }

    internal class UserDto
    {
        
    }
}