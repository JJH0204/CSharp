using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase : MonoBehaviour
{
    protected void DontDestroy<T>() where T : Object
    {
        // 문제1: Init Scene이 중복 호출될 경우 DontDestroyOnLoad로 인해 Manager가 중복 생성된다.
        T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);

        if (managers.Length > 1)
            // Debug.Log("Destroy");
            Destroy(gameObject);   // 중복된 SystemManager 삭제
            // Destroy 함수는 다음 프레임으로 연기되어 호출되므로, 현재 프레임에서는 삭제되지 않음
            // (다른 객체에서 참조해서 사용할 수 있어 에러가 발생할 여지가 있음)
        else
            DontDestroyOnLoad(gameObject);    // 씬이 바뀌어도 파괴되지 않도록 설정
    }
}
