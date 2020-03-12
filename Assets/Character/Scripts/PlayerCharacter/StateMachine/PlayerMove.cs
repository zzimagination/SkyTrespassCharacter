using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerMove : StateMachineBehaviour
    {
        STCharacterController characterController;
        PlayerAnimatorManager animatorManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if(!animatorManager)
                animatorManager = animator.GetComponent<PlayerAnimatorManager>();

            if(!characterController)
            {
                characterController = animator.GetComponent<STCharacterController>();
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //animatorManager._rigidbody.useGravity = true;
            //animatorManager._rigidbody.isKinematic = false;
            //animatorManager.TransformUpdate();
            //animatorManager.canPick = true;
            //animatorManager.canAttack = true;
            //animatorManager.canChangeWeapons = true;
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //animatorManager.MoveAddDelt();
            //animatorManager.RotateDelt();

            characterController.Move();
            characterController.Rotate();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            //animatorManager.canPick = false;
            //animatorManager.canAttack = false;
            //animatorManager.canChangeWeapons = false;
        }
    }
}