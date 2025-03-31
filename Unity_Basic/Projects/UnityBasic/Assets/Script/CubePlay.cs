using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CUBE_PLAY_STATE
{
    IDLE,
    MOVE_FRONT,
    MOVE_BACK,
    MOVE_LEFT,
    MOVE_RIGHT,
    ATTACK
}

public class CubePlay : MonoBehaviour
{
    private Animator animator; // cache
    private bool bIsAttack = false; // 공격 중인지 확인
    private Vector3 v3OriginPosition; // 초기 위치
    private CUBE_PLAY_STATE eCubePlayState = CUBE_PLAY_STATE.IDLE; // 현재 상태

    // [SerializeField] private float fSpeed = 10f; // 속도

    void Start()
    {
        animator = GetComponent<Animator>();
        v3OriginPosition = gameObject.transform.localPosition;


    }

    void Update()
    {
        float fHorizontal = Input.GetAxis("Horizontal");
        float fVertical = Input.GetAxis("Vertical");

        // HardAnimaitor(fHorizontal, fVertical);
        // SimpleAnimaitor(fHorizontal, fVertical);

        // Use FSM
        if (fVertical > 0)
        {
            ChangeState(CUBE_PLAY_STATE.MOVE_FRONT);
        }
        else if (fVertical < 0)
        {
            ChangeState(CUBE_PLAY_STATE.MOVE_BACK);
        }
        else if (fHorizontal > 0)
        {
            ChangeState(CUBE_PLAY_STATE.MOVE_RIGHT);
        }
        else if (fHorizontal < 0)
        {
            ChangeState(CUBE_PLAY_STATE.MOVE_LEFT);
        }
        else
        {
            ChangeState(CUBE_PLAY_STATE.IDLE);
        }

        if (Input.GetKeyDown(KeyCode.Space) && eCubePlayState != CUBE_PLAY_STATE.ATTACK)
        {
            ChangeState(CUBE_PLAY_STATE.ATTACK);
        }

        UpdateState();

        UseBGMBox();

        gameObject.transform.localPosition = v3OriginPosition;
    }

    private void UpdateState()
    {
        switch (eCubePlayState)
        {
            case CUBE_PLAY_STATE.MOVE_FRONT:
                animator.Play("MoveFront");
                break;
            case CUBE_PLAY_STATE.MOVE_BACK:
                animator.Play("MoveBack");
                break;
            case CUBE_PLAY_STATE.MOVE_LEFT:
                animator.Play("MoveLeft");
                break;
            case CUBE_PLAY_STATE.MOVE_RIGHT:
                animator.Play("MoveRight");
                break;
        }
    }

    private void ChangeState(CUBE_PLAY_STATE eState)
    {
        eCubePlayState = eState;

        switch (eCubePlayState)
        {
            case CUBE_PLAY_STATE.IDLE:
                animator.Play("Idle");
                break;
            // case CUBE_PLAY_STATE.MOVE_FRONT:
            //     // animator.Play("MoveFront");
            //     break;
            // case CUBE_PLAY_STATE.MOVE_BACK:
            //     // animator.Play("MoveBack");
            //     break;
            // case CUBE_PLAY_STATE.MOVE_LEFT:
            //     // animator.Play("MoveLeft");
            //     break;
            // case CUBE_PLAY_STATE.MOVE_RIGHT:
            //     // animator.Play("MoveRight");
            //     break;
            case CUBE_PLAY_STATE.ATTACK:
                animator.Play("Attack");
                StartCoroutine(CoroutineAttackFSM());
                break;
        }
    }

    private void UseBGMBox()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "BGM")
            {
                // Debug.Log("BGM Box");

                if (Input.GetKeyDown(KeyCode.R))
                {
                    collider.GetComponent<BGMPlay>().PlayBGM();
                }
                else if (Input.GetKeyDown(KeyCode.T))
                {
                    collider.GetComponent<BGMPlay>().ChangeBGM();
                }
                else if (Input.GetKeyDown(KeyCode.Y))
                {
                    collider.GetComponent<BGMPlay>().StopBGM();
                }
            }
        }
    }

    private void SimpleAnimaitor(float fHorizontal, float fVertical)
    {
        if (!bIsAttack)
        {
            if (fHorizontal != 0 || fVertical != 0)
            {
                if (fHorizontal > 0)
                {
                    animator.Play("MoveRight");
                }
                else if (fHorizontal < 0)
                {
                    animator.Play("MoveLeft");
                }

                if (fVertical > 0)
                {
                    animator.Play("MoveFront");
                }
                else if (fVertical < 0)
                {
                    animator.Play("MoveBack");
                }
            }
            else
            {
                animator.Play("Idle");
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Attack");
            bIsAttack = true;
            StartCoroutine(CoroutineAttack());
        }
    }

    private IEnumerator CoroutineAttack()
    {
        yield return new WaitForSeconds(0.5f);
        bIsAttack = false;
    }

    private IEnumerator CoroutineAttackFSM()
    {
        yield return new WaitForSeconds(0.5f);
        eCubePlayState = CUBE_PLAY_STATE.IDLE;
    }

    private void HardAnimaitor(float fHorizontal, float fVertical)
    {
        if (fVertical > 0)
        {
            animator.SetBool("IsMoveFront", true);
            animator.SetBool("IsMoveBack", false);
            animator.SetBool("IsMoveRight", false);
            animator.SetBool("IsMoveLeft", false);
        }
        else if (fVertical < 0)
        {
            animator.SetBool("IsMoveBack", true);
            animator.SetBool("IsMoveFront", false);
            animator.SetBool("IsMoveRight", false);
            animator.SetBool("IsMoveLeft", false);
        }
        else
        {
            animator.SetBool("IsMoveBack", false);
            animator.SetBool("IsMoveFront", false);
        }

        if (fHorizontal > 0)
        {
            animator.SetBool("IsMoveFront", false);
            animator.SetBool("IsMoveBack", false);
            animator.SetBool("IsMoveRight", true);
            animator.SetBool("IsMoveLeft", false);
        }
        else if (fHorizontal < 0)
        {
            animator.SetBool("IsMoveBack", false);
            animator.SetBool("IsMoveFront", false);
            animator.SetBool("IsMoveRight", false);
            animator.SetBool("IsMoveLeft", true);
        }
        else
        {
            animator.SetBool("IsMoveRight", false);
            animator.SetBool("IsMoveLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("IsAttack");
        }
    }


}
