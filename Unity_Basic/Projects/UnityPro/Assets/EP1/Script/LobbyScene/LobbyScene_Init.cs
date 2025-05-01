using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene_Init : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);    // 2초 대기
        SceneManager.LoadScene(SCENE_TYPE.Init.ToString());    // Init Scene으로 이동 (Init Scene이 중복 호출되는 상황 연출)
    }
}
