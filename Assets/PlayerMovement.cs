using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotatespeed;
    public float JumpForce;
    public bool isGrounded;

    private Rigidbody characterRigidbody;

    Vector2 jumplimit = new Vector2(0, 2f);

    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        isGround();
    }

    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        //�ε巯�� �������� �����ϰ� �ʹٸ� GetAxis, ������ �������� GetAxisRaw

        //Vector3 velocity = new Vector3(inputX, 0, inputZ);
        //velocity *= speed;
        //characterRigidbody.velocity = velocity;

        Vector3 dir = new Vector3(inputX, 0, inputZ);

        if (!(inputX == 0 && inputZ == 0))
        {
            //�̵��� ȸ���� ���ÿ� ó��
            transform.position += dir * speed * Time.deltaTime;
            //ȸ���ϴ� �κ�
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotatespeed);
        }
    }

    void Jump()
    {
        //transform.localPosition = ClampPosition(transform.localPosition);

        if (Input.GetKey(KeyCode.Space)&& isGrounded == true)
        {
            characterRigidbody.AddForce(Vector3.up * JumpForce);
        }
    }

    void isGround()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.red);
        if (Physics.Raycast(transform.position - (Vector3.down * 0.1f), Vector3.down, out hit, 0.3f))
        {
            if (hit.transform.tag == "Ground")
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
    }

    //public Vector3 ClampPosition(Vector3 position)
    //{
    //    return new Vector3(Mathf.Clamp(position.y, -jumplimit.y, jumplimit.y), 0, 0);
    //}
}
