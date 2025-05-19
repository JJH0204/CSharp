using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    #region Variables
    [Header("GameObjects")]
    [SerializeField] private GameObject Timer = null; // 타이머 UI 오브젝트
    [SerializeField] private GameObject Score = null; // 점수 UI 오브젝트
    #endregion

    #region Cache
    // 캐시된 컴포넌트들
    #endregion

    #region Unity Methods
    private void Awake()
    {

    }
    private void Start()
    {
        Init();
    }
    #endregion

    #region Custom Methods
    // 게임 UI 초기화 메서드
    public void Init()
    {
        // UI 초기화 작업 수행
        // 타이머 UI 초기화
        // TimeView timer = Timer.GetComponent<TimeView>();
        // timer.Init();

        // 점수 UI 초기화
        // ScoreView score = Score.GetComponent<ScoreView>();
        // score.Init();
    }
    // Have 버튼
    public void OnClick_CatchButton()
    {
        // Debug.Log("Have 버튼 클릭됨");
        GameManager.Instance.InputProcess(INPUT_TYPE.CATCH);
    }
    // Throw 버튼
    public void OnClick_ThrowButton()
    {
        // Debug.Log("Throw 버튼 클릭됨");
        GameManager.Instance.InputProcess(INPUT_TYPE.THROW);
    }
    #endregion
}
