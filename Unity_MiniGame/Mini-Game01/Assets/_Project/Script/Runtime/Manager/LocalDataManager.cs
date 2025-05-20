using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class GameData
{
    public string GameName;
    public string GameVersion;
    public int PointApple;
    public int PointGoldApple;
    public int PointRottenApple;
    public float TimeLimit;
}

public class LocalDataManager : ManagerBase
{
    #region Variables
    public GameData GameData { get; private set; } = null;
    #endregion
    public override bool Init()
    {
        // json 파일 로드
        GameData = LoadJsonFile();

        return IsInit = true;
    }

    private GameData LoadJsonFile()
    {
        if (string.IsNullOrEmpty(Config.GAME_DATA_PATH))
        {
            Debug.LogError("게임 데이터 경로가 설정되지 않았습니다.");
            return null;
        }

        string jsonFilePath = Config.GAME_DATA_PATH + "/GameData.json";
        if (!System.IO.File.Exists(jsonFilePath))
        {
            Debug.LogError("게임 데이터 파일이 존재하지 않습니다: " + jsonFilePath);
            return null;
        }

        string jsonData = System.IO.File.ReadAllText(jsonFilePath);
        GameData gameData;
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

        // 게임 데이터 초기화
        // Debug.Log($"게임 이름: {gameData.GameName}");
        // Debug.Log($"게임 버전: {gameData.GameVersion}");
        // Debug.Log($"사과 점수: {gameData.PointApple}");
        // Debug.Log($"금사과 점수: {gameData.PointGoldApple}");
        // Debug.Log($"썩은 사과 점수: {gameData.PointRottenApple}");
        // Debug.Log($"시간 제한: {gameData.TimeLimit}");
        return gameData;
    }

    #region Singleton
    private static LocalDataManager instance = null;
    public static LocalDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LocalDataManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("LocalDataManager");
                    instance = obj.AddComponent<LocalDataManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<LocalDataManager>();
    }
    #endregion
}
