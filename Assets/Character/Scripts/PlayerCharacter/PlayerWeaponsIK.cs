using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerWeaponsIK : StateMachineBehaviour
    {
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