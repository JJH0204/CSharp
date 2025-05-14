public enum PACKET_NAME_TYPE
{
    None,   // 패킷 이름 없음
    ApplicationConfig,  // 앱 버전, 환경, OS 타입을 서버에 요청하는 패킷 이름
    Maintenance,  // 서버 점검 패킷 이름
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

public enum LANGUAGE_TYPE
{
    //
    // 요약:
    //     Afrikaans.
    Afrikaans = 0,
    //
    // 요약:
    //     Arabic.
    Arabic = 1,
    //
    // 요약:
    //     Basque.
    Basque = 2,
    //
    // 요약:
    //     Belarusian.
    Belarusian = 3,
    //
    // 요약:
    //     Bulgarian.
    Bulgarian = 4,
    //
    // 요약:
    //     Catalan.
    Catalan = 5,
    //
    // 요약:
    //     Chinese.
    Chinese = 6,
    //
    // 요약:
    //     Czech.
    Czech = 7,
    //
    // 요약:
    //     Danish.
    Danish = 8,
    //
    // 요약:
    //     Dutch.
    Dutch = 9,
    //
    // 요약:
    //     English.
    English = 10,
    //
    // 요약:
    //     Estonian.
    Estonian = 11,
    //
    // 요약:
    //     Faroese.
    Faroese = 12,
    //
    // 요약:
    //     Finnish.
    Finnish = 13,
    //
    // 요약:
    //     French.
    French = 14,
    //
    // 요약:
    //     German.
    German = 15,
    //
    // 요약:
    //     Greek.
    Greek = 16,
    //
    // 요약:
    //     Hebrew.
    Hebrew = 17,
    [Obsolete("Use SystemLanguage.Hungarian instead (UnityUpgradable) -> Hungarian", true)]
    Hugarian = 18,
    //
    // 요약:
    //     Icelandic.
    Icelandic = 19,
    //
    // 요약:
    //     Indonesian.
    Indonesian = 20,
    //
    // 요약:
    //     Italian.
    Italian = 21,
    //
    // 요약:
    //     Japanese.
    Japanese = 22,
    //
    // 요약:
    //     Korean.
    Korean = 23,
    //
    // 요약:
    //     Latvian.
    Latvian = 24,
    //
    // 요약:
    //     Lithuanian.
    Lithuanian = 25,
    //
    // 요약:
    //     Norwegian.
    Norwegian = 26,
    //
    // 요약:
    //     Polish.
    Polish = 27,
    //
    // 요약:
    //     Portuguese.
    Portuguese = 28,
    //
    // 요약:
    //     Romanian.
    Romanian = 29,
    //
    // 요약:
    //     Russian.
    Russian = 30,
    //
    // 요약:
    //     Serbo-Croatian.
    SerboCroatian = 31,
    //
    // 요약:
    //     Slovak.
    Slovak = 32,
    //
    // 요약:
    //     Slovenian.
    Slovenian = 33,
    //
    // 요약:
    //     Spanish.
    Spanish = 34,
    //
    // 요약:
    //     Swedish.
    Swedish = 35,
    //
    // 요약:
    //     Thai.
    Thai = 36,
    //
    // 요약:
    //     Turkish.
    Turkish = 37,
    //
    // 요약:
    //     Ukrainian.
    Ukrainian = 38,
    //
    // 요약:
    //     Vietnamese.
    Vietnamese = 39,
    //
    // 요약:
    //     ChineseSimplified.
    ChineseSimplified = 40,
    //
    // 요약:
    //     ChineseTraditional.
    ChineseTraditional = 41,
    //
    // 요약:
    //     Hindi.
    Hindi = 42,
    //
    // 요약:
    //     Unknown.
    Unknown = 43,
    //
    // 요약:
    //     Hungarian.
    Hungarian = 18
}