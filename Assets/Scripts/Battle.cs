using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private Animator animator;
    public bool sheathe;

    public bool charge;
    private float chargeTimer;

    public bool attacking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
        Sheathe();
        ChargeAtk();
        animator.SetBool("Sheathe", sheathe);
    }

    void Sheathe()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!sheathe)
            {
                sheathe = true;
            }
            else if(sheathe)
            {
                sheathe = false;
            }

            animator.SetTrigger("SheatheButton");
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attacking = true;
            animator.SetTrigger("Attack");
        }
        else if (Input.GetMouseButtonUp(0) && chargeTimer >= 0.5f)
        {
            attacking = true;
            animator.SetTrigger("Upper");
        }
        else
        {
            attacking = false;
        }
    }

    void ChargeAtk()
    {
        animator.SetFloat("Charge", chargeTimer);

        if(Input.GetMouseButton(0))
        {
            charge = true;
            chargeTimer += Time.deltaTime;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            charge = false;
            chargeTimer = 0;
        }
    }
}
