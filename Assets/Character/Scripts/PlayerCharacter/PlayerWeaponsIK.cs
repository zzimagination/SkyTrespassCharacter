using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerWeaponsIK : StateMachineBehaviour
    {
        WeaponsRifle weaponsShoot;
        STCharacterController characterController;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            characterController = animator.GetComponent<STCharacterController>();
            var eq = animator.GetComponent<EquipmentManager>();
            weaponsShoot= eq.currentWeapons as WeaponsRifle;
            characterController.isIK = true;
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