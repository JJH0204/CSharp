using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : MonoBehaviour
{
    private Dictionary<int, List<GameObject>> dics = new Dictionary<int, List<GameObject>>();
    public GameObject GetObjectByPrefab(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        int hashCode = prefab.GetHashCode();

        if (!dics.ContainsKey(hashCode))
        {
            dics.Add(hashCode, new List<GameObject>());
        }

        bool isMonsterFound = false;
        List<GameObject> monsterAs = dics[hashCode];
        foreach (var item in monsterAs)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.transform.localPosition = position; // 위치 초기화
                item.gameObject.transform.localRotation = rotation; // 회전 초기화
                item.gameObject.SetActive(true); // 오브젝트 활성화
                return item;
            }
        }

        GameObject obj = Instantiate(prefab, parent);
        obj.transform.localPosition = position;
        obj.transform.localRotation = rotation;
        dics[hashCode].Add(obj);
        return obj;
    }
}
