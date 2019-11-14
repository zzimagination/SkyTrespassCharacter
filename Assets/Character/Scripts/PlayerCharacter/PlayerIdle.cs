using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace SkyTrespass.Character {
    public class PlayerIdle : StateMachineBehaviour
    {
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller= animator.GetComponent<STCharacterController>();
            controller.RotateDelt();
            controller.StopRigidbody(true);
        }


        public override void OnStateIK(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            var eq= animator.GetComponent<EquipmentManager>();
            if(eq)
            {
              
            }
        }
    }
}