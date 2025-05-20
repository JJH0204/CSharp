using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class InitSceneInit : MonoBehaviour
{
    #region Variables
    private List<ManagerBase> managers = null;
    private bool isInit = false;
    private int initCount = 0;

    [Header("Loading State")]
    [SerializeField] private int initMaxCount = 0;
    #endregion

    #region cache
    private InitSceneUI initSceneUI = null;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        initSceneUI = FindObjectOfType<InitSceneUI>();
        if (initSceneUI == null)
        {
            Debug.LogError("InitSceneUI not found");
            return;
        }

        // 메니저 리스트 초기화
        managers = new List<ManagerBase>
        {
            // 메니저 인스턴스 생성
            GameManager.Instance,
            InputManager.Instance,
            NoteSystemManager.Instance,
            SceneLoadManager.Instance,
            LocalDataManager.Instance
        };
    }
    private IEnumerator Start()
    {
        // 메니저 초기화 대기
        yield return new WaitForSeconds(0.5f);
        // 메니저 초기화
        foreach (var manager in managers)
        {
            isInit = manager.Init();
            if (!isInit)
            {
                Debug.LogError($"{manager.name} Init Failed");
                yield break;
            }
            initCount++;
            yield return new WaitForSeconds(0.5f);
            // 초기화 진행률 표시

            initSceneUI.SetLoadingText($"Loading... {initCount}/{initMaxCount}");
            initSceneUI.SetLoadingSlider((float)initCount / initMaxCount);
        }
        yield return new WaitForSeconds(0.5f);
        // 초기화 완료
        initSceneUI.SetLoadingText("Loading Complete");
        yield return new WaitForSeconds(0.5f);
        // isInit = true;
        // 씬 전환
        SceneLoadManager.Instance.LoadScene(SCENE_TYPE.TITLE);
    }
    #endregion
}
