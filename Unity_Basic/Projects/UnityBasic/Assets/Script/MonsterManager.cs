using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private GameObject monsterAPrefab;
    [SerializeField] private Transform monsterParent;

    private List<MonsterA> monsterAs = new List<MonsterA>(); // 몬스터를 생성하자마자 리스트에 추가해서 관리(최적화)
    void Start()
    {
        // 초기 몬스터 생성
        // GameObject obj = Instantiate(monsterAPrefab, monsterParent);
        // obj.transform.localPosition = Vector3.zero;
        // obj.transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // MonsterA[] monsterAs = monsterParent.GetComponentsInChildren<MonsterA>(true); // 자식 오브젝트에서 MonsterA 컴포넌트들을 찾기

            bool isMonsterFound = false;
            foreach (MonsterA monsterA in monsterAs)
            {
                if (!monsterA.gameObject.activeInHierarchy)
                {
                    monsterA.gameObject.transform.localPosition = Vector3.zero; // 위치 초기화
                    monsterA.gameObject.transform.localRotation = Quaternion.identity; // 회전 초기화
                    monsterA.gameObject.SetActive(true); // 오브젝트 활성화
                    isMonsterFound = true;
                }

                if (isMonsterFound == true)
                {
                    break; // 몬스터가 활성화되면 루프 종료
                }
            }

            if (!isMonsterFound)
            {
                // 몬스터 생성
                GameObject obj = Instantiate(monsterAPrefab, monsterParent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                monsterAs.Add(obj.GetComponent<MonsterA>()); // 생성한 몬스터를 리스트에 추가
            }
        }
    }
}
