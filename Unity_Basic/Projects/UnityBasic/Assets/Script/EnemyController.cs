using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // [SerializeField]
    // GameObject gameObject;
    // Start is called before the first frame update

    // private PlayerController playerController; // cache

    [SerializeField] private LayerMask layerMask; // 레이어 마스크

    void Start()
    {
        // playerController = FindAnyObjectByType<PlayerController>(); // PlayerController를 찾아서 가져온다.

        // Debug.Log(playerController.gameObject.name);

        InitLayerMask();
    }

    private void InitLayerMask()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player"); // 레이어 마스크를 설정한다. // 비트연산자로 레이어를 설정할 수 있다.

        // layerMask = 1 << 6;              // 0000 0000 0000 0000 0000 0000 0010 0000
        // layerMask = 1 << 0 | 1 << 5;     // 0000 0000 0000 0000 0000 0000 0001 0001
    }

    // Update is called once per frame
    void Update()
    {
        // LookAtPlayerUseCache();
        // LookAtPlayerUsePhysics();


        RotateWithRaycast();

    }

    private void RotateWithRaycast()
    {
        // gameObject.transform.rotation = Vector3.zero;
        // 회전값은 quaternion으로 정의하기 때문에 Vector 를 사용할 수 없다.
        gameObject.transform.Rotate(new Vector3(0, 5, 0));  // gameObject.transform.rotation *= Quaternion.Euler(new Vector3(0, 5, 0)); 
                                                            // Euler를 이용해 회전값을 vector 값을 이용해 설정할 수 있다. 
                                                            // * 연산으로 회전값을 누적시켜 회전 시킬 수 있다.

        // raycast 를 활용해 감시하다가 플레이어를 발견하면 경고
        // if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out RaycastHit hitInfo))
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out RaycastHit hitInfo, 10f, layerMask)) // LayerMask를 이용해 검사할 레이어를 설정할 수 있다. (최적화 방법)
        {
            if (hitInfo.collider.gameObject.tag == "Player")
            {
                Debug.Log("Hit! " + hitInfo.collider.gameObject.name);
                Debug.DrawLine(gameObject.transform.position, hitInfo.point, Color.red);    // 레이를 그려준다.
                hitInfo.collider.GetComponent<PlayerController>().Warning();
            }
        }
    }

    private void LookAtPlayerUseCache()
    {
        // float fDistance = Vector3.Distance(gameObject.transform.position, playerController.transform.position); // 플레이어와의 거리
        // // Debug.Log(fDistance);

        // if (fDistance <= 5f)
        // {
        //     // Attack();
        //     gameObject.transform.LookAt(playerController.transform); // 플레이어를 바라보게 한다.
        //     playerController.Warning();     // 플레이어에게 경고한다.
        // }
    }

    private void LookAtPlayerUsePhysics()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 5f); // 반경 5f 안에 있는 모든 콜라이더를 반환한다.
        foreach (Collider collider in colliders)
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
