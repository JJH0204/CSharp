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
    [SerializeField] private GameObject Combo = null; // 콤보 UI 오브젝트
    #endregion

    #region Cache
    // 캐시된 컴포넌트들
    private TimeView timeView = null; // 타이머 UI 컴포넌트
    private ScoreView scoreView = null; // 점수 UI 컴포넌트
    private ComboView comboView = null; // 콤보 UI 컴포넌트
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
        GameManager.Instance.InputProcess(INPUT_TYPE.CATCH);
    }
    // Throw 버튼
    public void OnClick_ThrowButton()
    {
        // Debug.Log("Throw 버튼 클릭됨");
        GameManager.Instance.InputProcess(INPUT_TYPE.THROW);
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
    #endregion
}
