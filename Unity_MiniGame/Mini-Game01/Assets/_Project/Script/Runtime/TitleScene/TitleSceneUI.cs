using UnityEngine;
using UnityEngine.Serialization;

public class TitleSceneUI : MonoBehaviour
{
    #region SerializeField
    
    [Header("Title Scene UI")]
    [FormerlySerializedAs("TitleLogo")] [SerializeField] private GameObject titleLogo; // 타이틀 로고
    [FormerlySerializedAs("StartButton")] [SerializeField] private GameObject startButton; // 시작 버튼
    [FormerlySerializedAs("ExitButton")] [SerializeField] private GameObject exitButton; // 종료 버튼
    [FormerlySerializedAs("OptionButton")] [SerializeField] private GameObject optionButton; // 옵션 버튼
    
    [Header("Popup")]
    [FormerlySerializedAs("SettingPopup")] [SerializeField] private GameObject settingPopup; // 설정 팝업
    #endregion

    #region OnClick Methods
    // 시작 버튼 클릭 시 호출되는 메서드
    public void OnClickStartButton()
    {
        // 게임 시작 로직
        // Debug.Log("게임 시작");
        SceneLoadManager.instance.LoadScene(SceneType.Game);
        
    }
    // 종료 버튼 클릭 시 호출되는 메서드
    public void OnClickExitButton()
    {
        // 게임 종료 로직
        // Debug.Log("게임 종료");
        Application.Quit();
    }
    // 옵션 버튼 클릭 시 호출되는 메서드
    public void OnClickOptionButton()
    {
        // 옵션 설정 로직
        Debug.Log("옵션 설정");
        // settingPopup.SetActive(true);
    }
    #endregion
}
