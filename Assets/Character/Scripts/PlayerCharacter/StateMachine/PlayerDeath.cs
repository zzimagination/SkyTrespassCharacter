using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerDeath : StateMachineBehaviour
    {

        STCharacterController controller;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            controller = animator.GetComponent<STCharacterController>();
            controller.InputSwitch(false);

            animator.SetBool("isDeath", true);
            animator.SetBool("attack", false);
        }
    }
}