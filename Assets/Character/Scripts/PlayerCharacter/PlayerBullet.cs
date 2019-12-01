using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerBullet : StateMachineBehaviour
    {
        STCharacterController characterController;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            characterController = animator.GetComponent<STCharacterController>();
            animator.SetLayerWeight(1, 1);
            characterController.isIK = false;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.SetLayerWeight(1, 0);
            characterController.isIK = true;
        }

    }
}