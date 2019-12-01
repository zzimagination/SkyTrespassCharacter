using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerShoot : PlayerWpAttack
    {
        WeaponsRifle weaponsShoot;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            PrepareAttack(animator);
            weaponsShoot = equipmentManager.currentWeapons as WeaponsRifle;
            characterController.isIK = true;
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
            if (characterController.isIK)
            {
                if (weaponsShoot)
                {
                    weaponsShoot.LeftIKAnimation(animator);
                }
            }
        }



    }
}