using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestA : MonoBehaviour
{
    [SerializeField] private GameObject testBPrefab;
    private GameObject testBInstance;
    private void Awake()
    {
        Debug.Log("TestA Awake");

        testBInstance = Instantiate(testBPrefab, Vector3.zero, Quaternion.identity);

        FindTestB(); // Awake 시점에 검색하려면 프리팹에서 활성화 상태가 True여야 함
    }

    void Start()
    {
        Debug.Log("TestA Start");

        // var testBInstance = FindAnyObjectByType<GameObject>(); // error
        testBInstance.SetActive(true);
        FindTestB();
    }

    private static void FindTestB()
    {
        TestB testB = FindAnyObjectByType<TestB>(); // 아니면 활성화 이후 검색해야 함
        if (testB != null)
        {
            Debug.Log("TestB found in TestA: " + testB.gameObject.name);
        }
        else
        {
            Debug.Log("TestB not found in TestA");
        }
    }
}
