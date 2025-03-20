using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    EnemyManager enemyManager;  // cache
                                // private void OnCollisionEnter(Collision collision)
                                // {
                                //     // collision.gameObject.SetActive(false);
                                //     Destroy(collision.gameObject);
                                // }
    private void Start()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>(); // EnemyManager를 찾아서 가져온다.
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyManager.Enemies.Remove(other.GetComponent<EnemyController>()); // 리스트에서 제거한다.
        Destroy(other.gameObject);  // 충돌한 오브젝트를 삭제한다.
    }
}
