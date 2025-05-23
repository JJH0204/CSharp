using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LocalDataManager : ManagerBase
{
    #region Variables
    public GameData gameData { get; private set; }
    public HistoryData historyData { get; private set; }
    #endregion

    #region Custom Methods

    private HistoryData LoadHistoryDataJsonFile()
    {
        if (string.IsNullOrEmpty(Config.GameDataPath))
        {
            Debug.LogError("게임 데이터 경로가 설정되지 않았습니다.");
            return null;
        }

        var jsonFilePath = Config.GameDataPath + "HistoryData.json";
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("게임 데이터 파일이 존재하지 않습니다: " + jsonFilePath);
            return null;
        }

        var jsonData = File.ReadAllText(jsonFilePath);
        
        try
        {
            historyData = JsonUtility.FromJson<HistoryData>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError("JSON 파싱 중 오류 발생: " + e.Message);
            return null;
        }

        if (historyData is not null) return historyData;
        Debug.LogError("게임 데이터 파싱에 실패했습니다. JSON 구조를 확인하세요.");
        return null;
    }
    // TODO: 점수 저장 메서드 구현
    public bool SaveHistoryDataJsonFile(UserData userData)
    {
        // 지정된 경로에 HistoryData 객체를 JSON 형식으로 저장합니다.
        if (string.IsNullOrEmpty(Config.GameDataPath))
        {
            Debug.LogError("게임 데이터 경로가 설정되지 않았습니다.");
            return false;
        }
        
        historyData.BestScore = historyData.BestScore < userData.totalScore ? userData.totalScore : historyData.BestScore;
        historyData.HighCombo = historyData.HighCombo < userData.currentCombo ? userData.currentCombo : historyData.HighCombo;
        
        const string jsonFilePath = Config.GameDataPath + "HistoryData.json";
        
        var jsonData = JsonUtility.ToJson(historyData, true);
        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            // Debug.Log("HistoryData 저장 완료: " + jsonFilePath);
            // UnityEditor.AssetDatabase.Refresh(); // TODO: 빌드 할때 에러가 발생한다.
        }
        catch (Exception e)
        {
            Debug.LogError("JSON 저장 중 오류 발생: " + e.Message);
            return false;
        }
        return true;
    }
    private GameData LoadGameDataJsonFile()
    {
        if (string.IsNullOrEmpty(Config.GameDataPath))
        {
            Debug.LogError("게임 데이터 경로가 설정되지 않았습니다.");
            return null;
        }

        const string jsonFilePath = Config.GameDataPath + "GameData.json";
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("게임 데이터 파일이 존재하지 않습니다: " + jsonFilePath);
            return null;
        }

        var jsonData = File.ReadAllText(jsonFilePath);
        
        try
        {
            gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError("JSON 파싱 중 오류 발생: " + e.Message);
            return null;
        }

        if (gameData == null)
        {
            Debug.LogError("게임 데이터 파싱에 실패했습니다. JSON 구조를 확인하세요.");
            return null;
        }

        if (gameData.TimeLimit <= 0)
        {
            Debug.LogError("TimeLimit 값이 유효하지 않습니다: " + gameData.TimeLimit);
            return null;
        }
        return gameData;
    }
    
    #endregion

    #region Singleton
    private static LocalDataManager _instance;
    public static LocalDataManager instance
    {
        get
        {
            if (_instance is not null) return _instance;
            _instance = FindObjectOfType<LocalDataManager>(); 
            if (_instance is not null) return _instance;
            var obj = new GameObject("LocalDataManager");
            _instance = obj.AddComponent<LocalDataManager>();
            return _instance;
        }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        DontDestroy<LocalDataManager>();
    }
    
    private void Start()
    {
        // 게임 데이터 로드
        // StartCoroutine(LoadGameData(result =>
        // {
        //     if (result == null) return;
        //     Debug.Log("GameData 로드 완료!");
        //     gameData = result;
        // }));
        #if UNITY_EDITOR
        // 게임 데이터 로드
        gameData = LoadGameDataJsonFile();
        if (gameData is null)
        {
            Debug.LogError("게임 데이터 로드 실패");
            return;
        }
        
        // 히스토리 데이터 로드
        historyData = LoadHistoryDataJsonFile();
        if (historyData is not null) return;
        Debug.LogError("히스토리 데이터 로드 실패");
        #elif UNITY_ANDROID || UNITY_WEBGL
        gameData = new GameData();
        gameData.TimeLimit = 60;
        gameData.PointApple = 10;
        gameData.PointGoldApple = 20;
        gameData.PointRottenApple = 50;
        #endif
    }
    #endregion
    
    #region SandBox
    
    string BuildStreamingPath(string filename)
    {
        return Path.Combine(Application.streamingAssetsPath, filename);
    }

    string BuildStreamingUri(string filename)
    {
        string path = BuildStreamingPath(filename);

        // Android = jar:file://…,  WebGL = http(s)://…  → 이미 URL 형태
#if UNITY_ANDROID || UNITY_WEBGL
    return path;
#else
        // Windows / macOS / Linux / 콘솔 → file:///C:/… 로 변환 + 슬래시 정규화
        return new Uri(path).AbsoluteUri;      // Uri 가 \ 를 / 로 바꿔 줌
#endif
    }
    
    public IEnumerator LoadGameData(Action<GameData> onLoaded)
    {
        // ‼️ Config 값 대신 플랫폼에 안전한 StreamingAssets 경로 사용
        var pathh = Path.Combine(Application.streamingAssetsPath, "GameData.json");
        var path = BuildStreamingPath("GameData.json");
        string json = null;

#if UNITY_ANDROID || UNITY_WEBGL
        // URL 전송 (Android=jar:file://… / WebGL=http(s)://…)
        using var req = UnityWebRequest.Get(path);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"게임 데이터 요청 실패: {req.error}");
            onLoaded?.Invoke(null);
            yield break;
        }
        json = req.downloadHandler.text;
#else
        // PC·콘솔은 File API 그대로 사용
        if (!File.Exists(path))
        {
            Debug.LogError($"게임 데이터 파일이 없습니다: {path}");
            onLoaded?.Invoke(null);
            yield break;
        }
        json = File.ReadAllText(path);
#endif

        // 공통 파싱·검증
        GameData data = null;
        try
        {
            data = JsonUtility.FromJson<GameData>(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"JSON 파싱 오류: {e.Message}");
        }

        if (data == null || data.TimeLimit <= 0)
        {
            Debug.LogError("게임 데이터 값이 유효하지 않습니다.");
            onLoaded?.Invoke(null);
            yield break;
        }
        onLoaded?.Invoke(data);          // 호출측에 결과 전달
    }
    
    #endregion
}


