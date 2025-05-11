public enum PACKET_NAME_TYPE
{
    None,   // 패킷 이름 없음
    ApplicationConfig,  // 앱 버전, 환경, OS 타입을 서버에 요청하는 패킷 이름
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

public enum RETURN_CODE
{
    OK = 200,
    ERROR = 400,
    NOT_FOUND = 404,
    SERVER_ERROR = 500,
}

public enum DEVELOPER_ID_AUTHORITY
{
    NONE = 0,
    TESTER = 1,
    SUPERUSER = 2,
}