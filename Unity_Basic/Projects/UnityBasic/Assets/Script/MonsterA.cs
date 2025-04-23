using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterA : MonoBehaviour
{
    [SerializeField][Range(10f, 20f)] private float fSpeed = 10.0f; // 몬스터 A 속도
    private void OnEnable() // 오브젝트가 활성화될 때 호출
    {
        // 몬스터 A 발사
        StartCoroutine(C_DisableObject());
    }

    // Update is called once per frame
    void Update()
    {
        // 몬스터 A 발사
        transform.Translate(Vector3.forward * Time.deltaTime * fSpeed);
    }

    private IEnumerator C_DisableObject()
    {
        yield return new WaitForSeconds(3.0f);
        // Destroy(this.gameObject);
        this.gameObject.SetActive(false); // 오브젝트 비활성화
    }
}
