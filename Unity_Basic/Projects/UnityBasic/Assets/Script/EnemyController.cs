using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // [SerializeField]
    // GameObject gameObject;
    // Start is called before the first frame update

    // private PlayerController playerController; // cache
    void Start()
    {
        // playerController = FindAnyObjectByType<PlayerController>(); // PlayerController를 찾아서 가져온다.

        // Debug.Log(playerController.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        #region Use Cache
        // float fDistance = Vector3.Distance(gameObject.transform.position, playerController.transform.position); // 플레이어와의 거리
        // // Debug.Log(fDistance);

        // if (fDistance <= 5f)
        // {
        //     // Attack();
        //     gameObject.transform.LookAt(playerController.transform); // 플레이어를 바라보게 한다.
        //     playerController.Warning();     // 플레이어에게 경고한다.
        // }
        #endregion

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 5f); // 반경 5f 안에 있는 모든 콜라이더를 반환한다.
        foreach(Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                gameObject.transform.LookAt(collider.transform.position); // 플레이어를 바라보게 한다.
                collider.GetComponent<PlayerController>().Warning();     // 플레이어에게 경고한다.
            }
        }
    }

    public void Attack()
    {
        Debug.Log($"{gameObject.name} Attack!");
    }
}
