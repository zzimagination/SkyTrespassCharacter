using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerShoot : PlayerWpAttack
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            PrepareAttack(animator);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdataAttack(animator,stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ExitAttack(animator);
            characterController.ChangeWeaponsEnd();
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {

            if (equipmentManager && equipmentManager.currentWeapons)
            {
                if (equipmentManager.currentWeapons.HasIK())
                {
                    var leftPos = equipmentManager.currentWeapons.leftIK.position;

                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftPos);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                }
            }
        }
    }
}