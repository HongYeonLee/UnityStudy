using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Vector2 lastMoveDirection = Vector2.right; // 기본적으로 오른쪽 방향으로 시작
    private bool isWalking = false; // 플레이어가 걷는지 여부

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInput();
        Animate();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        // 키 입력이 있을 때만 마지막으로 누른 키의 방향을 업데이트
        if (moveDirection != Vector2.zero)  
        {
            lastMoveDirection = moveDirection;
            isWalking = true;
        }
        else
        {
            // 키 입력이 없을 때는 가만히 있는 상태로 설정
            isWalking = false;
        }
    }

    void Move()
    {
        if (isWalking)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
        else
        {
            // 키 입력이 없을 때 가만히 있도록 속도를 0으로 설정
            rb.velocity = Vector2.zero;
        }
    }

    void Animate()
    {
        anim.SetBool("IsWalking", isWalking);
        anim.SetFloat("AnimMoveX", lastMoveDirection.x);
        anim.SetFloat("AnimMoveY", lastMoveDirection.y);
    }
}
