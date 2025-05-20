using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject TitleLogo = null; // 타이틀 로고
    [SerializeField] private GameObject StartButton = null; // 시작 버튼
    [SerializeField] private GameObject ExitButton = null; // 종료 버튼
    [SerializeField] private GameObject OptionButton = null; // 옵션 버튼
    #endregion

    #region OnClick Methods
    // 시작 버튼 클릭 시 호출되는 메서드
    public void OnClickStartButton()
    {
        // 게임 시작 로직
        // Debug.Log("게임 시작");
        // GameManager.Instance.GameState = GAME_STATE.PLAYING;
        // GameManager.Instance.Init();
        SceneLoadManager.Instance.LoadScene(SCENE_TYPE.GAME);
        
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
    }
    #endregion
}
