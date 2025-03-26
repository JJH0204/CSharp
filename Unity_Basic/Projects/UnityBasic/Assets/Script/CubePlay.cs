using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlay : MonoBehaviour
{
    private Animator animator; // cache
    private bool bIsAttack = false; // 공격 중인지 확인
    private Vector3 v3OriginPosition; // 초기 위치

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
        SimpleAnimaitor(fHorizontal, fVertical);

        gameObject.transform.localPosition = v3OriginPosition;
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
