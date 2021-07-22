using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PressurePlateScript>().ChangeOffset(-0.2f);
    }
}
