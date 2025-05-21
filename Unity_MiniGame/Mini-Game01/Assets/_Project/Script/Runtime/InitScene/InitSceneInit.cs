using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneInit : MonoBehaviour
{
    #region Variables
    
    private List<ManagerBase> _managers;     // cache
    private InitSceneUI _initSceneUI;        // cache
    private bool _isInstance;
    
    #endregion
    
    #region Inspector Variables
    
    [Header("Managers")]
    [SerializeField] private int initMaxCount;
    [SerializeField] private GameObject objInitSceneUI;
    
    #endregion


    #region Unity Methods
    private void Awake()
    {
        // 메니저 리스트 초기화
        _managers = new List<ManagerBase>
        {
            // 메니저 인스턴스 생성
            GameManager.instance,
            InputManager.instance,
            SceneLoadManager.instance,
            LocalDataManager.instance
            // PopupManager.Instance
        };
        
        _initSceneUI = objInitSceneUI.GetComponent<InitSceneUI>();
        if (_initSceneUI is null)
        {
            Debug.LogError("InitSceneUI not found");
        }
        else if (_managers.Count <= 0 || _managers == null)
        {
            Debug.LogError("Manager List is Empty");
        }
        else
        {
            Debug.Log("InitSceneInit Awake");
        }
    }
    private void Start()
    {
        StartCoroutine(InstanceManager()) ;
    }

    #endregion
    
    #region Custom Methods
    
    // 게임 진행에 필요한 매니저들을 인스턴스화 하는 코루틴
    private IEnumerator InstanceManager()
    {
        int initCount = 0;
        // 메니저 생성 대기
        yield return new WaitForSeconds(0.5f);
        // 메니저 생성
        foreach (var manager in _managers)
        {
            _isInstance = manager.IsInstance();
            if (!_isInstance)
            {
                Debug.LogError($"{manager.name} Instance Failed");
                yield break;
            }
            initCount++;
            yield return new WaitForSeconds(0.5f);

            _initSceneUI.SetLoadingText($"Loading... {initCount}/{initMaxCount}");
            _initSceneUI.SetLoadingSlider((float)initCount / initMaxCount);
        }
        yield return new WaitForSeconds(0.5f);
        // 생성 완료
        _initSceneUI.SetLoadingText("Loading Complete");
        yield return new WaitForSeconds(0.5f);
        
        // 씬 전환
        SceneLoadManager.instance.LoadScene(SceneType.Title);
    }
    
    #endregion
}
