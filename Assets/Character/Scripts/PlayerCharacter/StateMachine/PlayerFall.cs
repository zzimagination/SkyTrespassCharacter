using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character {
    public class PlayerFall :StateMachineBehaviour
    {
        STCharacterController characterController;
        PlayerAnimatorManager animatorManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if(!animatorManager)
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            if (!characterController)
                characterController = animator.GetComponent<STCharacterController>();
            characterController.SetAim(false);
           
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {

        }
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
        }
    }
}