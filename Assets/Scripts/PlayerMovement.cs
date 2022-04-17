using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterState
{
    Stand, Move, Jump, Attack
}

public class PlayerMovement : MonoBehaviour
{
    //[EnumFlags] 
    public ECharacterState state;

    [SerializeField] float speed;
    [SerializeField] float rotatespeed;
    [SerializeField] float moveSpeed;

    [SerializeField] float JumpForce;

    float JumpDelay;
    float jumpMax = 2.0f;

    float chargeTimer;

    [SerializeField] bool sheathe;
    [SerializeField] bool charge;
    [SerializeField] bool isGrounded;
    [SerializeField] bool jumpAble;

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
        Attack();
        Sheathe();
        ChargeAtk();
        animator.SetBool("Sheathe", sheathe);
    }

    void LateUpdate()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            state = ECharacterState.Stand;
        }
    }

    void PlayerMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        moveSpeed = Mathf.Clamp(moveSpeed, 0f, 0.2f);

        //�ε巯�� �������� �����ϰ� �ʹٸ� GetAxis, ������ �������� GetAxisRaw

        Vector3 dir = new Vector3(inputX, 0, inputZ);

        animator.SetFloat("Velocity.X", inputX);
        animator.SetFloat("Velocity.Z", inputZ);

        if (!(inputX == 0 && inputZ == 0))
        {
            //������ state ����
            state = ECharacterState.Move;

            moveSpeed += Time.deltaTime;
            animator.SetFloat("MoveSpeed", moveSpeed);

            //�̵��� ȸ���� ���ÿ� ó��
            transform.position += dir * speed * Time.deltaTime;
            //ȸ���ϴ� �κ�
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
            if(state != ECharacterState.Attack)
            {
                state = ECharacterState.Jump;

                if (jumpAble == true)
                {
                    characterRigidbody.AddForce(Vector3.up * JumpForce);
                    animator.SetBool("Jump", true);
                }
            }
        }

        //���� ���� ����
        if (jumpMax <= transform.localPosition.y)
        {
            animator.SetBool("Jump", false);
            characterRigidbody.AddForce(Vector3.up * (Physics.gravity.y * 7));
        }

        if (JumpDelay >= 0.3f)
        {
            jumpAble = true;
        }
        else
        {
            jumpAble = false;
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
        JumpDelay = 0;
        animator.SetBool("isGrounded", false);
        animator.SetBool("Jump", false);
    }

    void Sheathe()
    {
        if(state == 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!sheathe)
                {
                    sheathe = true;
                }
                else
                {
                    sheathe = false;
                }

                animator.SetTrigger("SheatheButton");
            }
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = ECharacterState.Attack;
            animator.SetTrigger("Attack");
        }
        else if (Input.GetMouseButtonUp(0) && chargeTimer >= 0.5f)
        {
            state = ECharacterState.Attack;
            animator.SetTrigger("Upper");
        }
    }

    void ChargeAtk()
    {
        animator.SetFloat("Charge", chargeTimer);

        if (Input.GetMouseButton(0))
        {
            charge = true;
            chargeTimer += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            charge = false;
            chargeTimer = 0;
        }
    }
}
