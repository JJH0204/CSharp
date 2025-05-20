public enum NOTE_TYPE
{
    APPLE,
    GOLDAPPLE,
    ROTTENAPPLE,
}

public enum INPUT_TYPE
{
    THROW,
    CATCH,
}

public enum SCENE_TYPE
{
    NONE,
    INIT,
    TITLE,
    GAME,
    GAMEOVER,
}

public enum GAME_STATE
{
    NONE,       // 없음
    INIT,       // 초기화
    READY,      // 준비
    PLAYING,    // 플레이 중
    PAUSE,      // 일시 정지
    GAMEOVER,   // 게임 오버
    END,        // 종료
}

public class Config
{
    public const string GAME_DATA_PATH = "Assets/_Project/Script/Runtime/Data/";
}