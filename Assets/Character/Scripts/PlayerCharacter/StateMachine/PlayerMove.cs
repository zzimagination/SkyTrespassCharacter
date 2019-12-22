using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerMove : StateMachineBehaviour
    {
        PlayerAnimatorManager animatorManager;
        STCharacterController controller;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            controller = animator.GetComponent<STCharacterController>();
            animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            //animatorManager.physics_MoveSpeed = animatorManager.moveSpeed;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animatorManager.TransformUpdate();
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller.StopRigidbody(false);
            animatorManager.MoveAddDelt();
            animatorManager.RotateDelt();
        }
    }
}