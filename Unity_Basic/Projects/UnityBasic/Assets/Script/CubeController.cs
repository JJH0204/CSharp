using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float fSpeed = 10f; // 이동 속도

    private void Start()
    {
        Application.targetFrameRate = 60;   // 프레임 제한 (60fps)


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
