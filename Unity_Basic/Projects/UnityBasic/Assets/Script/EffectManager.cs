using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gEffectPrefabs; // 이펙트 프리팹
    [SerializeField] private float[] fDestoryTime; // 이펙트 소멸 시간
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayEffect(int nIndex, Transform tParent)
    {
        if (gEffectPrefabs.Length > nIndex)
        {
            GameObject gEffect = Instantiate(gEffectPrefabs[nIndex], tParent);
            gEffect.transform.localRotation = Quaternion.Euler(-90, 0, 0);          // 이펙트 방향 x -90 으로 회전
            StartCoroutine(CoroutineDestroyEffect(gEffect, fDestoryTime[nIndex]));
        }
        else
        {
            Debug.LogError("Invalid effect index: " + nIndex);
        }
    }

    private IEnumerator CoroutineDestroyEffect(GameObject gEffect, float fDestoryTime)
    {
        yield return new WaitForSeconds(fDestoryTime);
        Destroy(gEffect);
    }
}
