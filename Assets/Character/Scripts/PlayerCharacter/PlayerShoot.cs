using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class PlayerShoot : StateMachineBehaviour
    {
        bool isChangeArm;

        float attackTimer;
        bool attackOnce;
        AttackMachine attackMachine;
        EquipmentManager equipmentManager;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            equipmentManager = animator.GetComponent<EquipmentManager>();
            attackMachine = animator.GetComponent<AttackMachine>();
            animator.SetLayerWeight(1, 1);
            attackTimer = attackMachine.attackCD;
            attackOnce = false;
            attackMachine = animator.GetComponent<AttackMachine>();
            equipmentManager = animator.GetComponent<EquipmentManager>();
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            bool attack= animator.GetBool("attack");
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
            }else
            {
                if(attackOnce==false)
                {
                    attackMachine.GunAttack();
                    attackOnce = true;
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
                animator.SetLayerWeight(1, 0);
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