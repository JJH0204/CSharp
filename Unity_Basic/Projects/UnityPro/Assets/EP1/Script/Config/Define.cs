// public class PacketName
// {
//     public const string ApplicationConfig = "ApplicationConfig"; // 앱 버전, 환경, OS 타입을 서버에 요청하는 패킷 이름
// }

public enum PACKET_NAME_TYPE
{
    ApplicationConfig,  // 앱 버전, 환경, OS 타입을 서버에 요청하는 패킷 이름
}

public enum SCENE_TYPE
{
    Init,
    Lobby,
    InGame,
    Loading,
}

public enum ENVIRONMENT_TYPE
{
    DEV = 0,
    STAGE = 1,
    LIVE = 2,
}

public enum OS_TYPE
{
    Android = 0,
    IOS = 10,
    PC = 20,
}