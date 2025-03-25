using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // [SerializeField]
    // GameObject gameObject;
    // Start is called before the first frame update

    // private PlayerController playerController; // cache

    [SerializeField] private LayerMask layerMask;   // 레이어 마스크
    [SerializeField] private Transform tTarget;     // 타겟
    private Vector3 vOriginPos;                     // 원점
    private float fFactor;
    private int iDgree = 0;                         // 각도
                                                    // private int iIndexMove;
                                                    // private bool isFinished;

    // IEnumerator Start()
    
    PlayerController playerController;
    NavMeshAgent navmashAgent;

    void Start()
    {
        #region
        // playerController = FindAnyObjectByType<PlayerController>(); // PlayerController를 찾아서 가져온다.

        // Debug.Log(playerController.gameObject.name);

        // InitLayerMask();

        // vOriginPos = gameObject.transform.position; // 원점을 설정한다.
        // tTarget = FindAnyObjectByType<PlayerController>().transform; // 타겟을 설정한다.

        // yield return StartCoroutine(CoroutineMoveRight());   // 코루틴 함수 실행 방법
        // yield return new WaitForSeconds(5f); // 1초 대기
        // yield return StartCoroutine(CoroutineMoveDown());

        // gameObject.transform.position = new Vector3(0, 0, 5);            // 월드 좌표계를 이용해 이동동
        // gameObject.transform.position = transform.TransformPoint(0, 0, 5);  // 로컬 좌표계를 이용해 이동
        #endregion
        
        // using cache
        playerController = FindAnyObjectByType<PlayerController>(); // PlayerController를 찾아서 가져온다.
        navmashAgent = GetComponent<NavMeshAgent>();                // NavMeshAgent 컴포넌트를 가져온다.
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
        #region
        // LookAtPlayerUseCache();
        // LookAtPlayerUsePhysics();

        // RotateWithRaycast();

        // MovementUseTrigonometric();
        // MovementUseLinearInterpolation();

        // 코루틴을 이용한 ㄱ자 이동
        // if (iIndexMove <= 3)
        // {
        //     gameObject.transform.Translate(1, 0, 0);
        //     iIndexMove++;
        // }
        // else
        // {
        //     isFinished = true;
        // }
        // if (isFinished)
        // {
        //     gameObject.transform.Translate(0, 0, -1);
        // }
        // 복잡한 이동 패턴을 코루틴을 이용해 구현
        #endregion

        navmashAgent.SetDestination(playerController.transform.position); // NavMeshAgent를 이용해 플레이어의 위치를 갱신 및 추적
    }

    IEnumerator CoroutineMoveRight()
    {
        yield return null;      // 다음 프레임까지 대기
        gameObject.transform.Translate(1, 0, 0);
        yield return null;
        gameObject.transform.Translate(1, 0, 0);
        yield return null;
        gameObject.transform.Translate(1, 0, 0);

        int iCount = 0;
        while (true)    // 프레임 단위로 반복복
        {
            if (iCount >= 3)    // 조건이 충족되면 코루틴 종료
            {
                yield break;
            }
            gameObject.transform.Translate(0, 0, 1);
            yield return null;
        }
    }

    IEnumerator CoroutineMoveDown()
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        gameObject.transform.Translate(0, 0, -1);
        yield return new WaitForSeconds(2f);
        gameObject.transform.Translate(0, 0, -1);
        yield return new WaitForSeconds(3f);
        gameObject.transform.Translate(0, 0, -1);
    }

    // 선형보간을 이용한 이동
    private void MovementUseLinearInterpolation()
    {
        fFactor += Time.deltaTime;  // fFactor = 0 - 1 사이의 값 (0: 원점, 1: 타겟)
        gameObject.transform.position = Vector3.Lerp(vOriginPos, tTarget.position, fFactor); // Lerp 함수를 이용해 원점과 타겟 사이를 이동한다.

        // 이 방식은 타겟 설정을 외부에서 받아야 하기 때문에 씬에 오브젝트를 올려서 설정해야 한다. (단점)
    }

    public void SetTarget(Transform tTarget)
    {
        this.tTarget = tTarget;
    }

    private void MovementUseTrigonometric()
    {
        // Debug.Log(Mathf.Cos(Mathf.Deg2Rad * 60)); // Cos 함수는 라디안 값을 받는다. 따라서 Deg2Rad를 이용해 각도를 라디안으로 변환한다.
        // transform.position = new Vector3(Mathf.Cos(Mathf.Deg2Rad * iDgree), gameObject.transform.position.y, 0);
        transform.position = new Vector3(Mathf.Cos(Mathf.Deg2Rad * iDgree), gameObject.transform.position.y, Mathf.Sin(Mathf.Deg2Rad * iDgree));    // Sin 파와 Cos 파를 이용해 원운동을 한다.
        iDgree++; // 5도씩 증가시킨다.
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
