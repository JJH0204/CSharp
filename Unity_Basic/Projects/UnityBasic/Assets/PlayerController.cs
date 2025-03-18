using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 파일 이름과 클래스 이름이 같아야 한다.
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;   // 프레임 제한 (60fps)
        Debug.Log("Hello World! - A Start");  // 콘솔에 출력 (시작시 한번만 출력)
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hello World! - Update"); // 콘솔에 출력 (프레임마다 출력)
    }
}
