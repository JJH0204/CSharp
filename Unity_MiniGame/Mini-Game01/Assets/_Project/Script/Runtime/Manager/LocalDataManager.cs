using System;
using System.IO;
using UnityEngine;

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

        string jsonFilePath = Config.GameDataPath + "/HistoryData.json";
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("게임 데이터 파일이 존재하지 않습니다: " + jsonFilePath);
            return null;
        }

        string jsonData = File.ReadAllText(jsonFilePath);
        
        try
        {
            historyData = JsonUtility.FromJson<HistoryData>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError("JSON 파싱 중 오류 발생: " + e.Message);
            return null;
        }

        if (historyData == null)
        {
            Debug.LogError("게임 데이터 파싱에 실패했습니다. JSON 구조를 확인하세요.");
            return null;
        }
        
        return historyData;
    }

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
        
        string jsonFilePath = Config.GameDataPath + "/HistoryData.json";
        
        string jsonData = JsonUtility.ToJson(historyData, true);
        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.Log("HistoryData 저장 완료: " + jsonFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("JSON 저장 중 오류 발생: " + e.Message);
            return false;
        }

        UnityEditor.AssetDatabase.Refresh();
        return true;
    }

    private GameData LoadGameDataJsonFile()
    {
        if (string.IsNullOrEmpty(Config.GameDataPath))
        {
            Debug.LogError("게임 데이터 경로가 설정되지 않았습니다.");
            return null;
        }

        string jsonFilePath = Config.GameDataPath + "/GameData.json";
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("게임 데이터 파일이 존재하지 않습니다: " + jsonFilePath);
            return null;
        }

        string jsonData = File.ReadAllText(jsonFilePath);
        
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
            if (_instance is null)
            {
                _instance = FindObjectOfType<LocalDataManager>();
                if (_instance is null)
                {
                    GameObject obj = new GameObject("LocalDataManager");
                    _instance = obj.AddComponent<LocalDataManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<LocalDataManager>();
    }
    
    void Start()
    {
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
    }
    #endregion
}


