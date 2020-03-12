using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerPickUp : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        STCharacterController characterController;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!animatorManager)
                animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            if (!characterController)
                characterController = animator.GetComponent<STCharacterController>();

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            characterController.PickEnd();
            animator.ResetTrigger("pick");
        }
    }
}