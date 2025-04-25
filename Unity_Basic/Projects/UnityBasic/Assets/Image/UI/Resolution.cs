using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // UI 캔버스의 사이즈를 동적으로 관리하는 스트립트
    void Update()
    {
        Camera camera = GetComponent<Camera>();
        var rect = camera.rect;
        var scaleheight = ((float)Screen.width / (float)Screen.height) / (16f / 9f); // 16:9 비율을 기준으로 설정
        var scalewidth = 1f / scaleheight;
        if (scaleheight < 1f)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }
}
