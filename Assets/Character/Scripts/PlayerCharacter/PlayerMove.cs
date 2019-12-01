using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerMove : StateMachineBehaviour
    {
        STCharacterController controller;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            controller = animator.GetComponent<STCharacterController>();
            
        }
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller.StopRigidbody(false);
            controller.MoveAddDelt();
            controller.RotateDelt();
        }
    }
}