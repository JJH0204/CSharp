using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    Camera mainCamera; // cache
    Vector3 vOriginPos; // 원래 위치

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>(); // Camera 컴포넌트를 가져온다.
    }

    public void Shake(float fSecond, float fMagnitude)
    {
        vOriginPos = mainCamera.transform.localPosition;    // 원래 위치를 저장한다.
        StopCoroutine("CourutineShake");                    // 연속해서 카메라 쉐이크 함수가 호출되면 기존의 코루틴을 종료하고 새로운 코루틴을 시작한다.
        ResetPosition();
        StartCoroutine(CorutineShake(fSecond, fMagnitude));
    }

    private IEnumerator CorutineShake(float fSecond, float fMagnitude)
    {
        float fTime = 0;
        while (true)
        {
            if (fTime >= fSecond)
            {
                break;
            }
            Vector3 vOffset = Random.insideUnitCircle * fMagnitude;
            gameObject.transform.localPosition += new Vector3(vOffset.x, vOffset.y, 0f);

            fTime += Time.deltaTime;
            yield return null;
        }

        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.localPosition = vOriginPos; // 원래 위치로 돌아간다.
    }
}
