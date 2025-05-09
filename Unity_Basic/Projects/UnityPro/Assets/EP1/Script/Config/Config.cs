using UnityEngine;

public class Config
{
    // DEV, STAGE, LIVE 환경을 구분하기 위한 전처리 지문을 설정합니다.
    // ctrl + shift + b 를 눌러서 빌드 설정을 열고, 
    // player settings>other settings>script compilation > Scripting define symbols에 추가합니다.
    public static readonly string APP_VERSION = Application.version; // 앱 버전

#if DEV
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.DEV; // DEV, STAGE, LIVE
    public const string SERVER_APP_CONFIG_URL = "http://localhost:5254";
#elif STAGE
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.STAGE; // DEV, STAGE, LIVE
    public const string SERVER_APP_CONFIG_URL = "https://localhost:7025/";
#elif LIVE
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.LIVE; // DEV, STAGE, LIVE
    public const string SERVER_APP_CONFIG_URL = "https://localhost:7025/";
#else
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.DEV; // DEV, STAGE, LIVE
    public const string SERVER_APP_CONFIG_URL = "https://localhost:7025/";
#endif

#if UNITY_ANDROID
    public const OS_TYPE E_OS_TYPE = OS_TYPE.Android; // Android, IOS, PC
#elif UNITY_IOS
    public const OS_TYPE E_OS_TYPE = OS_TYPE.IOS; // Android, IOS, PC
#else
    public const OS_TYPE E_OS_TYPE = OS_TYPE.PC; // Android, IOS, PC
#endif
}

