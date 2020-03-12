using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerChangeWeapons : StateMachineBehaviour
    {
        public PlayerAnimatorManager animatorManager;
        public STCharacterController characterController;
        bool isSet;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!animatorManager)
                animatorManager = animator.GetComponent<PlayerAnimatorManager>();
            if (!characterController)
                characterController = animator.GetComponent<STCharacterController>();

            var iks= animator.GetBehaviours<PlayerIK>();
            for (int i = 0; i < iks.Length; i++)
            {
                iks[i].isIk = false;
            }
            isSet = false;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > 0.5f)
            {
                if (!isSet)
                {
                    animator.SetInteger("weaponsType", animatorManager.newWeaponsType);
                    characterController.DoingChangeWeaons();
                    isSet = true;
                }
                if (stateInfo.normalizedTime > 0.8f)
                    animator.SetLayerWeight(1, (1 - stateInfo.normalizedTime) * 5);
            }
            else
            {
                if (stateInfo.normalizedTime < 0.2f)
                {
                    animator.SetLayerWeight(1, stateInfo.normalizedTime * 5);
                }
            }
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetLayerWeight(1, 0);
            var iks = animator.GetBehaviours<PlayerIK>();
            for (int i = 0; i < iks.Length; i++)
            {
                iks[i].isIk = true;
            }

            animator.ResetTrigger("changeWeapons");
        }
    }
}