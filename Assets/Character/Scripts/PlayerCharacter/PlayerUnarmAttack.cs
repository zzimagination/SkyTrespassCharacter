using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace SkyTrespass.Character
{
    public class PlayerUnarmAttack : StateMachineBehaviour
    {
        float attackTimer;
        AttackMachine attackMachine;
        STCharacterController characterController;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            characterController = animator.GetComponent<STCharacterController>();
            attackMachine = animator.GetComponent<AttackMachine>();
            animator.SetLayerWeight(1, 1);
            characterController.EnterAttack();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(stateInfo.normalizedTime-attackTimer>attackMachine.fistAttackCD)
            {
                attackTimer++;
                attackMachine.FistAttack();
            }


        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            animator.SetLayerWeight(1, 0);
            characterController.ExitAttack();
            characterController.ChangeWeaponsEnd();
            
        }
    }
}