using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGear : MonoBehaviour
{
    public float fTime = 0.0f;

    public void OnEnable()
    {
        gameObject.SetActive(true);
    }
    public void OnDisable()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        fTime += Time.deltaTime;
        if (fTime > 0.06f)
        {
            fTime = 0.0f;
            transform.Rotate(new Vector3(0, 0, -30f));
        }
    }
}
