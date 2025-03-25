using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 파일 이름과 클래스 이름이 같아야 한다.
public class PlayerController : MonoBehaviour
{
    EnemyManager enemyManager;  // cache
    [SerializeField] private float fSpeed = 10f; // 속도
    private Rigidbody rBody; // Rigidbody 컴포넌트

    private bool bIsJumping = false; // 점프 중인지 확인

    float fYRotation = 0f; // Y축 회전값
    float fXRotation = 0f; // X축 회전값
    CameraShake cameraShake; // cache

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;   // 프레임 제한 (60fps)
                                            // Debug.Log("Hello World! - A Start");  // 콘솔에 출력 (시작시 한번만 출력)

        // enemyManager = FindObjectOfType<EnemyManager>(); // EnemyManager를 찾아서 가져온다. (직접 연결해서 사용할 수 없을 때 사용)
        // enemyManager = FindAnyObjectByType<EnemyManager>(); // FindObjectOfType의 개선 버전전
        // enemyManager.AttackAll();       // EnemyManager의 AttackAll 함수 호출

        // 게임 오브젝트의 위치를 수정할 수 있다.
        // Debug.Log(gameObject.transform.position);
        // gameObject.transform.position = new Vector3(0, 0, 0); // 위치 변경
        // Debug.Log(gameObject.transform.position);

        enemyManager = FindAnyObjectByType<EnemyManager>(); // EnemyManager를 찾아서 가져온다.

        cameraShake = FindAnyObjectByType<CameraShake>(); // CameraShake를 찾아서 가져온다.
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateTransform();       // Transform.Position을 이용해 이동
        // UpdateRigidbody();       // Rigidbody를 이용해 이동
        MoveFinal();
        Jump();                 // Rigidbody를 이용한 점프
        MouseControll();        // 마우스 컨트롤

        // UseCache4Attack(); // Cache를 이용한 공격
        UsePhysics4Attack(); // Physics를 이용한 공격

    }

    private void UseCache4Attack()
    {
        // Way 1
        foreach (var enemy in enemyManager.Enemies)
        {
            float fDistance = Vector3.Distance(gameObject.transform.position, enemy.transform.position); // 플레이어와의 거리
            if (fDistance <= 3f)
            {
                // gameObject.transform.LookAt(enemy.transform); // 적을 바라보게 한다.
                // enemy.Warning(); // 적에게 경고한다.
                enemy.Attack();     // 적을 공격한다.
            }
        }
    }

    private void UsePhysics4Attack()
    {
        // // Way 2: Cache 없이
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 3f); // 반경 3f 안에 있는 모든 콜라이더를 반환한다.

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                // gameObject.transform.LookAt(collider.transform.position); // 적을 바라보게 한다.
                // collider.GetComponent<EnemyController>().Warning(); // 적에게 경고한다.
                collider.GetComponent<EnemyController>().Attack(); // 적을 공격한다.
            }
        }
    }

    private void UpdateTransform()
    {
        #region Use Transform.Position to Move
        // Debug.Log("Hello World! - Update"); // 콘솔에 출력 (프레임마다 출력)

        // 프레임 마다 오브젝트에 위치값을 추가
        // gameObject.transform.position += new Vector3(0, 0, 0.01f); // 위치 변경
        // 프레임 마다 실행속도가 다를 수 있다.
        // Debug.Log(Time.deltaTime);

        // position 값을 이용해 위치를 이동 시킬 경우 프레임마다 이동거리가 다르다.
        // 특정 프레임은 3초가 걸리고 특정 프레임은 1초가 걸렸는데 둘다 동일하게 고정된 vector 값 만큼 이동한다.
        // 이를 보정하기 위해 Time.deltaTime을 사용한다. ( 백터값 * Time.deltaTime )
        // gameObject.transform.position += new Vector3(0, 0, 0.01f) * Time.deltaTime * fSpeed; // 위치 변경 (속도 적용)

        float fHorizontal = Input.GetAxis("Horizontal"); // 좌우 이동
        float fVertical = Input.GetAxis("Vertical"); // 상하 이동

        // Debug.Log($"Horizontal: {fHorizontal}, Vertical: {fVertical}");

        Vector3 vMove = new Vector3(fHorizontal, 0, fVertical); // 이동값 계산
        Vector3 vMoveNormalized = vMove.normalized * Time.deltaTime * fSpeed; // 정규화된 벡터값
        gameObject.transform.Translate(vMoveNormalized);    // gameObject.transform.position += vMoveNormalized; // 위치 변경 (속도 적용)
        #endregion
    }

    private void UpdateRigidbody()
    {
        rBody = gameObject.GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져온다.

        float fHorizontal = Input.GetAxis("Horizontal"); // 좌우 이동
        float fVertical = Input.GetAxis("Vertical"); // 상하 이동

        Vector3 vMove = new Vector3(fHorizontal, 0, fVertical); // 이동값 계산
        Vector3 vMoveNormalized = vMove.normalized * fSpeed; // 정규화된 벡터값 // Time.deltaTime을 곱하지 않는다.

        rBody.velocity = new Vector3(vMoveNormalized.x, rBody.velocity.y, vMoveNormalized.z); // 속도 적용
    }

    private void MoveFinal()
    {
        float fHorizontal = Input.GetAxis("Horizontal"); // 좌우 이동
        float fVertical = Input.GetAxis("Vertical"); // 상하 이동

        Vector3 vMov = new Vector3(fHorizontal, 0, fVertical);
        Vector3 vWorldPos = transform.TransformDirection(vMov);                     // 로컬 좌표계를 월드 좌표계로 변환
        transform.Translate(vWorldPos * Time.deltaTime * fSpeed, Space.World);      // 이동
    }

    private void MouseControll()
    {
        float fMouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100; // 마우스 좌우 이동
        float fMouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 500; // 마우스 상하 이동

        // Debug.Log($"Mouse X: {fMouseX}, Mouse Y: {fMouseY}");

        // 마우스 움직임을 통해 화면을 움직이려면 Y축을 회전시켜야 한다.

        fYRotation += fMouseX; // Y축 회전값 계산
        fXRotation -= fMouseY; // X축 회전값 계산

        // x축 회전값을 제한한다.( 90도 ~ -90도 )
        Mathf.Clamp(fXRotation, -90, 90);

        gameObject.transform.rotation = Quaternion.Euler(fXRotation, fYRotation, 0); // 회전값 적용
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !bIsJumping) // 점프 // Space 키 또는 joystick button 3 (Y)
        {
            Debug.Log("Jump!");
            rBody.velocity = new Vector3(rBody.velocity.x, 10f, rBody.velocity.z); // 최대 속도 제한
            // rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse); // 위로 힘을 가한다.
            bIsJumping = true; // 점프 중
        }
        // Rigidbody 를 사용하지 않고 Transform 을 사용할 경우 중력가속도 등을 직접 구현해야 하는 어려움이 있때 문에 Rigidbody 를 사용한다.
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log($"OnCollisionEnter: {other.gameObject.name}");

        if (other.gameObject.tag == "Enemy")
        {
            // Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyController>().Attack(); // 충돌할 게임 오브젝트의 EnemyController 컴포넌트의 Attack 함수 호출

            // 프레임 사이에 충돌이 일어나면 처리가 안될 수 있다.
            // Rigidbody 의 Interpolate 값을 Interpolate 로 변경하면 해결할 수 있다.
        }

        if (other.gameObject.name == "Plane")
        {
            bIsJumping = false; // 점프 중이 아님
        }
    }

    public void Warning()
    {
        Debug.Log("Warning!");
    }

    public void Attack()
    {
        Debug.Log("Attack!");
        
        cameraShake.Shake(0.1f, 0.5f); // 카메라 쉐이크
    }
}
