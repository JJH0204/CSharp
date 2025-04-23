using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private GameObject monsterAPrefab;
    [SerializeField] private GameObject monsterBPrefab;
    [SerializeField] private Transform monsterParent;

    private ObjPoolManager objPoolManager; // cache

    // private List<MonsterA> monsterAs = new List<MonsterA>(); // 몬스터를 생성하자마자 리스트에 추가해서 관리(최적화)
    void Start()
    {
        objPoolManager = FindAnyObjectByType<ObjPoolManager>(); // ObjPoolManager를 찾기
        // 초기 몬스터 생성
        // GameObject obj = Instantiate(monsterAPrefab, monsterParent);
        // obj.transform.localPosition = Vector3.zero;
        // obj.transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = objPoolManager.GetObjectByPrefab(monsterAPrefab, monsterParent, Vector3.zero, Quaternion.identity);

            // Debug.Log(obj.GetHashCode()); // 오브젝트 고유 식별 번호
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject obj = objPoolManager.GetObjectByPrefab(monsterBPrefab, monsterParent, Vector3.zero, Quaternion.identity);

            // Debug.Log(obj.GetHashCode()); // 오브젝트 고유 식별 번호
        }
    }
}
