using TMPro;
using UnityEngine;

public class EndingPopup : MonoBehaviour
{
    #region SerializeField
    [Header("TMP Text")]
    [SerializeField] private TextMeshProUGUI totalScoreTMP;
    [SerializeField] private TextMeshProUGUI bestScoreTMP;
    [SerializeField] private TextMeshProUGUI currentComboTMP;
    [SerializeField] private TextMeshProUGUI highComboTMP;
    
    [Header("Button")]
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject goTitleButton;
    [SerializeField] private GameObject exitButton;
    #endregion
    
    #region Variables

    private int _totalScore;
    private int _bestScore;
    private int _currentCombo;
    private int _highCombo;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (!CheckSerializedField())
        {
            Debug.LogError($"{nameof(EndingPopup)} requires a {nameof(CheckSerializedField)}");
        }
    }
    
    #endregion
    
    #region Private Methods

    private bool CheckSerializedField()
    {
        if (totalScoreTMP is null || bestScoreTMP is null || currentComboTMP is null || highComboTMP is null)
        {
            Debug.LogError("TMP Text is null");
            return false;
        }
        
        if (restartButton is null || goTitleButton is null || exitButton is null)
        {
            Debug.LogError("Button is null");
            return false;
        }
        
        // Debug.Log("SerializedField Check Success");
        return true;
    }

    #endregion
    
    #region OnClick Methods
    
    public void OnClickRestartButton()
    {
        // 게임 매니저를 통해 게임을 재시작합니다.
        GameManager.instance.RestartGame();
        gameObject.SetActive(false);
    }
    
    public void OnClickGoTitleButton()
    {
        // 게임을 종료하고 타이틀 씬으로 이동합니다.
        SceneLoadManager.instance.LoadScene(SceneType.Init);
    }
    
    public void OnClickExitButton()
    {
        // // 기록을 저장하고 종료합니다.
        Application.Quit();
    }
    
    #endregion

    #region Public Methods

    public void SetUserData(UserData userData)
    {
        this._bestScore = userData.bestScore;
        this._highCombo = userData.highCombo;
        this._currentCombo = userData.currentCombo;
        this._totalScore = userData.totalScore;
        
        bestScoreTMP.text = _bestScore.ToString();
        highComboTMP.text = _highCombo.ToString();
        currentComboTMP.text = _currentCombo.ToString();
        totalScoreTMP.text = _totalScore.ToString();
    }

    #endregion
}
