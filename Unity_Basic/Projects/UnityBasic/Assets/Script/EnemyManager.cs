using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefeb; // 프리팹을 이용해 오브젝트를 생성합니다.
    private List<EnemyController> enemyControllers = new List<EnemyController>();     // 직접 연결하여 속도가 빠릅니다.

    // 외부에서 접근할 수 있도록 프로퍼티를 사용합니다.
    public List<EnemyController> Enemies
    {
        get
        {
            return enemyControllers;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++) // 반복문으로 적 4마리를 생성합니다.
        {
            GameObject gameObject = Instantiate(enemyPrefeb, this.gameObject.transform);    // 프리팹을 이용해 오브젝트를 생성합니다.
                                                                                            // EnemyController 의 자식으로 생성
            EnemyController enemyController = gameObject.GetComponent<EnemyController>();   // EnemyController 컴포넌트를 가져옵니다.
            enemyControllers.Add(enemyController); // 리스트에 추가합니다.
        }
        // Debug.Log(gameObject.name); // 현재 스크립트가 붙어있는 게임 오브젝트의 이름을 반환합니다.

        // transform은 게임 오브젝트의 위치, 회전, 크기 등을 나타내는 컴포넌트입니다.
        // Debug.Log(transform.position);
        // Debug.Log(transform.rotation);
        // Debug.Log(transform.localScale);

        // 게임 오브젝트에 붙어있는 BoxCollider 컴포넌트를 반환합니다.
        // var boxCollider = GetComponent<BoxCollider>();

        // 자식 게임 오브젝트를 가져옵니다.
        // EnemyController[] enemyControllers = gameObject.GetComponentsInChildren<EnemyController>();  // 자식에서 찾기 때문에 비용은 상대적으로 낮다.

        // FindObjectsOfType는 컴포넌트를 기준으로로 Scene에 존재하는 모든 오브젝트를 검색합니다.
        // enemyControllers = FindObjectsOfType<EnemyController>(); // 비용이 많이 들어가고, true 로 인자를 전달하지 않으면 숨겨진 오브젝트는 검색하지 않습니다.

        // FindGameObjectsWithTag는 오브젝트의 Tag를 기준으로로 Scene에 존재하는 모든 오브젝트를 검색합니다.
        // GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy"); // 비용이 많이 들어가고, 숨겨진 오브젝트는 검색하지 않습니다.

        // foreach (var gameObject in gameObjects)
        // {
        //     enemyControllers.Add(gameObject.GetComponent<EnemyController>());
        // }

        // 확인용
        // foreach (var enemyController in enemyControllers)
        // {
        //     Debug.Log(enemyController.gameObject.name);
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // public void AttackAll()
    // {
    //     foreach (var enemyController in enemyControllers)
    //     {
    //         enemyController.Attack();
    //     }
    // }
}
