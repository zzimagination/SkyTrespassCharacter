using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{

    public class PlayerWpAttack : StateMachineBehaviour
    {
        public float attackTiming;
        protected bool isChangeArm;

        protected float attackTimer;
        protected bool attackOnce;
        protected AttackMachine attackMachine;
        protected EquipmentManager equipmentManager;
        protected STCharacterController characterController;

        protected void PrepareAttack(Animator animator)
        {
            characterController = animator.GetComponent<STCharacterController>();
            equipmentManager = characterController.equipment;
            attackMachine = characterController.attackMachine;

            characterController.EnterAttack();
            animator.SetLayerWeight(1, 1);
            attackTimer = 0;
        }

        protected void UpdataAttack(Animator animator, AnimatorStateInfo state)
        {
            if (state.normalizedTime - attackTimer > attackTiming)
            {
                attackTimer++;
                attackMachine.GunAttack();
            }
           
        }

        protected void ExitAttack(Animator animator)
        {
            characterController.ExitAttack();
            if (animator.GetBool("changeAim"))
                animator.SetBool("changeAim", false);

        }
    }
}