using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotatespeed;
    private float moveSpeed;

    public bool isGrounded;

    public float JumpForce;
    private float JumpDelay;
    public bool jumpAble;

    private float jumpMax = 2.0f;

    private Rigidbody characterRigidbody;
    private Animator animator;

    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
        isGround();
    }

    void Update()
    {
        
    }

    void PlayerMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        moveSpeed = Mathf.Clamp(moveSpeed, 0f, 0.2f);

        //부드러운 움직임을 구현하고 싶다면 GetAxis, 딱딱한 움직임은 GetAxisRaw

        Vector3 dir = new Vector3(inputX, 0, inputZ);

        animator.SetFloat("Velocity.X", inputX);
        animator.SetFloat("Velocity.Z", inputZ);

        if (!(inputX == 0 && inputZ == 0))
        {
            moveSpeed += Time.deltaTime;
            animator.SetFloat("MoveSpeed", moveSpeed);

            //이동과 회전을 동시에 처리
            transform.position += dir * speed * Time.deltaTime;
            //회전하는 부분
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotatespeed);
        }
        else
        {
            moveSpeed -= Time.deltaTime;
            animator.SetFloat("MoveSpeed", moveSpeed);
        }
    }

    void PlayerJump()
    {
        animator.SetFloat("JumpHeight", transform.position.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            if(jumpAble==true)
            {
                characterRigidbody.AddForce(Vector3.up * JumpForce);
                animator.SetBool("Jump", true);
            }
        }

        //점프 높이 제한
        if (jumpMax <= transform.localPosition.y)
        {
            animator.SetBool("Jump", false);
            characterRigidbody.AddForce(Vector3.down * (-Physics.gravity.y*2));
        }

        if(JumpDelay>=0.3f)
        {
            jumpAble = true;
            return;
        }
    }

    void isGround()
    {
        JumpDelay = Mathf.Clamp(JumpDelay, 0, 0.5f);

        RaycastHit hit;

        Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.red);
        if (Physics.Raycast(transform.position - (Vector3.down * 0.1f), Vector3.down, out hit, 0.3f))
        {
            if (hit.transform.tag == "Ground")
            {
                JumpDelay += Time.deltaTime;

                isGrounded = true;
                animator.SetBool("isGrounded", true);
                return;
            }
        }
        isGrounded = false;
        jumpAble = false;
        JumpDelay = 0;
        animator.SetBool("isGrounded", false);
        animator.SetBool("Jump", false);
    }
}
