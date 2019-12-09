using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerDeath : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        STCharacterController controller;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            animatorManager.EnterDeathInvoke();
            //controller = animator.GetComponent<STCharacterController>();
            //controller.InputSwitch(false);

            animator.SetBool("isDeath", true);
            animator.SetBool("attack", false);
        }
    }
}