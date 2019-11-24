using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{

    public class PlayerWpAttack : StateMachineBehaviour
    {
        protected bool isChangeArm;

        protected float attackTimer;
        protected bool attackOnce;
        protected AttackMachine attackMachine;
        protected EquipmentManager equipmentManager;
        protected STCharacterController characterController;

        protected void PrepareAttack(Animator animator)
        {
            characterController = animator.GetComponent<STCharacterController>();
            equipmentManager = animator.GetComponent<EquipmentManager>();
            attackMachine = animator.GetComponent<AttackMachine>();

            characterController.EnterAttack();
            animator.SetLayerWeight(1, 1);
            attackTimer = attackMachine.attackCD;
            attackOnce = false;
        }

        protected void UpdataAttack(Animator animator)
        {
            bool attack = animator.GetBool("attack");
            if (attack)
            {
                if (attackTimer > attackMachine.attackCD)
                {
                    attackTimer = 0;
                    attackMachine.GunAttack();
                    attackOnce = true;
                }
                else
                {
                    attackTimer += Time.deltaTime;
                }
            }
            else
            {
                if (attackOnce == false)
                {
                    attackMachine.GunAttack();
                    attackOnce = true;
                }
            }
        }

        protected void ExitAttack(Animator animator)
        {
            characterController.ExitAttack();
            animator.SetLayerWeight(1, 0);
        }
    }
}