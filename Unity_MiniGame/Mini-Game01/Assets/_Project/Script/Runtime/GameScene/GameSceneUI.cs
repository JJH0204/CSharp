using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// using UnityEngine.Playables;

public class GameSceneUI : MonoBehaviour
{
    #region SerializeField
    [Header("GameObjects")]
    [SerializeField] private GameObject Timer = null; // 타이머 UI 오브젝트
    [SerializeField] private GameObject Score = null; // 점수 UI 오브젝트
    [SerializeField] private GameObject Combo = null; // 콤보 UI 오브젝트
    
    [Header("Popup Prefabs")]
    [SerializeField] private GameObject PausePopupPrefab = null; // 일시정지 팝업
    [SerializeField] private GameObject EndingPopupPrefab = null; // 게임 종료 팝업
    #endregion

    #region Cache
    // 캐시된 컴포넌트들
    private TimeView timeView = null; // 타이머 UI 컴포넌트
    private ScoreView scoreView = null; // 점수 UI 컴포넌트
    private ComboView comboView = null; // 콤보 UI 컴포넌트
    
    private GameObject EndingPopup;
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
        // Debug.Log("GameSceneUI Init() 호출됨");
        timeView = Timer.GetComponent<TimeView>();
        scoreView = Score.GetComponent<ScoreView>();
        comboView = Combo.GetComponent<ComboView>();

        timeView.Init();
        scoreView.Init();
        comboView.Init();
    }
    // Have 버튼
    public void OnClick_CatchButton()
    {
        // Debug.Log("Have 버튼 클릭됨");
        GameManager.instance.InputProcess(InputType.Catch);
    }
    // Throw 버튼
    public void OnClick_ThrowButton()
    {
        // Debug.Log("Throw 버튼 클릭됨");
        GameManager.instance.InputProcess(InputType.Throw);
    }

    public void SetScore(int score)
    {
        // Debug.Log("SetScore() 호출됨");
        scoreView.Score = score;
    }
    public void SetCombo(int combo)
    {
        // Debug.Log("SetCombo() 호출됨");
        comboView.Combo = combo;
    }

    public void SetTime(float time)
    {
        // Debug.Log("SetTime() 호출됨");
        timeView.StartTimer(time);
    }
    
    public void SetEndingPopup(bool isActive, UserData userData, Action onClickRestart, Action onClickGoTitle, Action onClickExit)
    {
        // Debug.Log("SetEndingPopup() 호출됨");
        if (EndingPopupPrefab is not null && EndingPopup is null)
        {
            EndingPopup = Instantiate(EndingPopupPrefab, transform);
            EndingPopup.SetActive(isActive);
            EndingPopup.GetComponent<EndingPopup>().SetUserData(userData);
            EndingPopup.GetComponent<EndingPopup>().SetButtonActions(onClickRestart, onClickGoTitle, onClickExit);
        }
    }
    
    #endregion
}
