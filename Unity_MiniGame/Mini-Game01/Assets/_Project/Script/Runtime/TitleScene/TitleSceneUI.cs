using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneUI : MonoBehaviour
{
    #region SerializeField
    [Header("Title Scene UI")]
    [SerializeField] private GameObject TitleLogo = null; // 타이틀 로고
    [SerializeField] private GameObject StartButton = null; // 시작 버튼
    [SerializeField] private GameObject ExitButton = null; // 종료 버튼
    [SerializeField] private GameObject OptionButton = null; // 옵션 버튼
    
    [Header("Popup")]
    [SerializeField] private GameObject SettingPopup = null; // 설정 팝업
    #endregion

    #region OnClick Methods
    // 시작 버튼 클릭 시 호출되는 메서드
    public void OnClickStartButton()
    {
        // 게임 시작 로직
        // Debug.Log("게임 시작");
        // GameManager.Instance.gameState = GAME_STATE.PLAYING;
        // GameManager.Instance.Init();
        SceneLoadManager.instance.LoadScene(SceneType.Game);
        
    }
    // 종료 버튼 클릭 시 호출되는 메서드
    public void OnClickExitButton()
    {
        // 게임 종료 로직
        Debug.Log("게임 종료");
        Application.Quit();
    }
    // 옵션 버튼 클릭 시 호출되는 메서드
    public void OnClickOptionButton()
    {
        // 옵션 설정 로직
        Debug.Log("옵션 설정");
        SettingPopup.SetActive(true);
    }
    #endregion
}
