using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : StateMachineBehaviour
{
    public ECharacterState state;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        state = GameObject.Find("Player").GetComponent<PlayerMovement>().state;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        state = ECharacterState.Stand;
    }
}
