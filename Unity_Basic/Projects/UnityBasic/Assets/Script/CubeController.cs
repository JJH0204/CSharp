using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float fSpeed = 10f; // 이동 속도
    // [SerializeField] private GameObject gEffectPrefab;
    [SerializeField] private Transform tEffectPosition;

    private EffectManager effectManager; // 이펙트 매니저

    private void Start()
    {
        Application.targetFrameRate = 60;   // 프레임 제한 (60fps)

        // GameObject gEffect = Instantiate(gEffectPrefab, tEffectPosition);       // 이펙트 프리팹을 tEffectPosition 위치에 생성

        effectManager = FindAnyObjectByType<EffectManager>();
        effectManager.PlayEffect(0, tEffectPosition); // 이펙트 매니저에서 이펙트 재생
    }

    private void Update()
    {
        float fHorizontal = Input.GetAxis("Horizontal");
        float fVertical = Input.GetAxis("Vertical");

        Vector3 v3Move = new Vector3(fHorizontal, 0, fVertical);
        Vector3 v3WorldPosition = transform.TransformDirection(v3Move);
        // transform.position += v3WorldPosition * fSpeed * Time.deltaTime;
        transform.Translate(v3WorldPosition * fSpeed * Time.deltaTime, Space.World);
    }
}
