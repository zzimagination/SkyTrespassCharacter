using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerShoot : StateMachineBehaviour
    {
        bool isChangeArm;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.SetLayerWeight(1, 1);

        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //if(!animator.GetComponent<STCharacterController>().keepAttack)
                animator.SetLayerWeight(1, 0);
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            var eq = animator.GetComponent<EquipmentManager>();
            if (eq)
            {
                var leftPos = eq.currentWeapons.leftIK.position;
                var rightPos = eq.currentWeapons.rightIK.position;

                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftPos);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
               
            }
        }
    }
}