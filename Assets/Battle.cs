using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private Animator animator;
    public bool sheathe = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Attack();
        Sheathe();
        animator.SetBool("Sheathe", sheathe);
    }

    void Sheathe()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if(!sheathe)
            {
                sheathe = true;
                animator.SetTrigger("SheatheButton");
            }
            else
            {
                sheathe = false;
                animator.SetTrigger("SheatheButton");
            }
        }
    }

    void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("Attack");
        }
    }
}
