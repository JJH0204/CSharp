using UnityEngine;
using UnityEngine.Serialization;

// using UnityEngine.Playables;

public class GameSceneUI : MonoBehaviour
{
    #region SerializeField
    
    [Header("GameObjects")]
    [FormerlySerializedAs("Timer")] [SerializeField] private GameObject timer; // 타이머 UI 오브젝트
    [FormerlySerializedAs("Score")] [SerializeField] private GameObject score; // 점수 UI 오브젝트
    [FormerlySerializedAs("Combo")] [SerializeField] private GameObject combo; // 콤보 UI 오브젝트
    
    [Header("Popup Prefabs")]
    [SerializeField] private GameObject pausePopupPrefab; // 일시정지 팝업
    [FormerlySerializedAs("EndingPopupPrefab")] [SerializeField] private GameObject endingPopupPrefab; // 게임 종료 팝업
    #endregion

    #region Cache
    // 캐시된 컴포넌트들
    private TimeView _timeView; // 타이머 UI 컴포넌트
    private ScoreView _scoreView; // 점수 UI 컴포넌트
    private ComboView _comboView; // 콤보 UI 컴포넌트
    
    private GameObject _endingPopup;
    #endregion

    #region Unity Methods

    private void Start()
    {
        Init();
    }
    
    #endregion

    #region Custom Methods
    // 게임 UI 초기화 메서드
    private void Init()
    {
        // Debug.Log("GameSceneUI Init() 호출됨");
        _timeView = timer.GetComponent<TimeView>();
        _scoreView = score.GetComponent<ScoreView>();
        _comboView = combo.GetComponent<ComboView>();

        _timeView.Init();
        _scoreView.Init();
        _comboView.Init();
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

    public void SetScore(int nScore)
    {
        // Debug.Log("SetScore() 호출됨");
        _scoreView.Score = nScore;
    }
    public void SetCombo(int nCombo)
    {
        // Debug.Log("SetCombo() 호출됨");
        _comboView.Combo = nCombo;
    }

    public void SetTime(float time)
    {
        // Debug.Log("SetTime() 호출됨");
        _timeView.StartTimer(time);
    }
    
    public void SetEndingPopup(bool isActive, UserData userData)
    {
        // Debug.Log("SetEndingPopup() 호출됨");
        if (_endingPopup is null)
        {
            if (endingPopupPrefab is not null)
                _endingPopup = Instantiate(endingPopupPrefab, transform);
            else
                Debug.LogError($"{nameof(endingPopupPrefab)} is null");
        }
        _endingPopup.SetActive(isActive);
        _endingPopup.GetComponent<EndingPopup>().SetUserData(userData);
        
        // if (endingPopupPrefab is not null && _endingPopup is null)
        // {
        //     _endingPopup = Instantiate(endingPopupPrefab, transform);
        //     _endingPopup.SetActive(isActive);
        //     _endingPopup.GetComponent<EndingPopup>().SetUserData(userData);
        // }
    }

    public void ResetEndingPopup()
    {
        if (_endingPopup is not null)
        {
            Destroy(_endingPopup.gameObject);
            _endingPopup = null;
        }
    }
    
    #endregion
}
