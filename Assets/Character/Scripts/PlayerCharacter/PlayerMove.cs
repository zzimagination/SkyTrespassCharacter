using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerMove : StateMachineBehaviour
    {
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
         
            var controller= animator.GetComponent<STCharacterController>();
            controller.MoveAddDelt();
            controller.RotateDelt();
            controller.StopRigidbody(false);
        }
    }
}